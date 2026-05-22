using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Orders;

namespace ShopForge.Api.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly ShopForgeDbContext _db;

    public OrderService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<OrderDto>> CreateOrderAsync(int userId, CreateOrderRequest request)
    {
        var address = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == request.ShippingAddressId && a.UserId == userId);
        if (address == null) return ApiResponse<OrderDto>.Fail("Shipping address not found.");

        var shippingMethod = await _db.ShippingMethods.FirstOrDefaultAsync(s => s.Id == request.ShippingMethodId && s.IsActive);
        if (shippingMethod == null) return ApiResponse<OrderDto>.Fail("Shipping method not found.");

        var cart = await _db.Carts
            .Include(c => c.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Images)
            .Include(c => c.Items).ThenInclude(i => i.ProductVariant)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.Items.Any())
            return ApiResponse<OrderDto>.Fail("Cart is empty.");

        // Validate stock
        foreach (var item in cart.Items)
        {
            var available = item.ProductVariantId.HasValue
                ? item.ProductVariant?.StockQuantity ?? 0
                : item.Product.StockQuantity;
            if (item.Quantity > available)
                return ApiResponse<OrderDto>.Fail($"Insufficient stock for '{item.Product.Name}'.");
        }

        decimal subTotal = cart.Items.Sum(i => i.UnitPrice * i.Quantity);
        decimal shippingCost = shippingMethod.FreeShippingThreshold.HasValue && subTotal >= shippingMethod.FreeShippingThreshold.Value
            ? 0 : shippingMethod.Cost;

        decimal discount = 0;
        string? couponCode = null;
        int? couponId = null;

        if (!string.IsNullOrEmpty(request.CouponCode))
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(c =>
                c.Code.ToUpper() == request.CouponCode.ToUpper() && c.IsActive);
            if (coupon != null)
            {
                discount = coupon.DiscountType == "Percentage"
                    ? Math.Round(subTotal * (coupon.DiscountValue / 100), 2)
                    : coupon.DiscountValue;
                if (coupon.MaximumDiscountAmount.HasValue && discount > coupon.MaximumDiscountAmount.Value)
                    discount = coupon.MaximumDiscountAmount.Value;
                couponCode = coupon.Code;
                couponId = coupon.Id;
                coupon.UsageCount++;
            }
        }

        decimal taxAmount = Math.Round((subTotal - discount) * 0.15m, 2);
        decimal total = subTotal + shippingCost + taxAmount - discount;

        var order = new Order
        {
            UserId = userId,
            OrderNumber = "ORD-TEMP",
            Status = "Pending",
            ShippingAddressId = address.Id,
            SubTotal = subTotal,
            ShippingCost = shippingCost,
            TaxAmount = taxAmount,
            DiscountAmount = discount,
            TotalAmount = total,
            CouponId = couponId,
            CouponCode = couponCode,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        order.OrderNumber = $"ORD-{order.CreatedAt:yyyyMMdd}-{order.Id:D5}";

        foreach (var cartItem in cart.Items)
        {
            _db.OrderItems.Add(new OrderItem
            {
                OrderId = order.Id,
                ProductId = cartItem.ProductId,
                ProductVariantId = cartItem.ProductVariantId,
                ProductName = cartItem.Product.Name,
                VariantName = cartItem.ProductVariant?.Name,
                SKU = cartItem.ProductVariant?.SKU ?? cartItem.Product.SKU,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.UnitPrice,
                TotalPrice = cartItem.UnitPrice * cartItem.Quantity,
                CreatedAt = DateTime.UtcNow
            });
        }

        _db.OrderStatusHistories.Add(new OrderStatusHistory
        {
            OrderId = order.Id,
            Status = "Pending",
            Note = "Order placed.",
            CreatedAt = DateTime.UtcNow
        });

        // Clear cart
        _db.CartItems.RemoveRange(cart.Items);

        await _db.SaveChangesAsync();

        return await GetOrderByIdAsync(order.Id);
    }

    public async Task<ApiResponse<OrderDto>> GetOrderByIdAsync(int orderId, int? userId = null)
    {
        var order = await LoadOrderQuery()
            .FirstOrDefaultAsync(o => o.Id == orderId && (userId == null || o.UserId == userId));
        if (order == null) return ApiResponse<OrderDto>.Fail("Order not found.");
        return ApiResponse<OrderDto>.Ok(MapToDto(order));
    }

    public async Task<ApiResponse<OrderDto>> GetOrderByNumberAsync(string orderNumber, int? userId = null)
    {
        var order = await LoadOrderQuery()
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber && (userId == null || o.UserId == userId));
        if (order == null) return ApiResponse<OrderDto>.Fail("Order not found.");
        return ApiResponse<OrderDto>.Ok(MapToDto(order));
    }

    public async Task<ApiResponse<PagedResult<OrderSummaryDto>>> GetUserOrdersAsync(int userId, int page, int pageSize)
    {
        var query = _db.Orders
            .Include(o => o.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Images)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return ApiResponse<PagedResult<OrderSummaryDto>>.Ok(new PagedResult<OrderSummaryDto>
        {
            Items = items.Select(MapToSummary).ToList(),
            Page = page, PageSize = pageSize, TotalCount = total
        });
    }

    public async Task<ApiResponse<PagedResult<OrderSummaryDto>>> GetAllOrdersAsync(int page, int pageSize, string? status, string? search)
    {
        var query = _db.Orders
            .Include(o => o.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Images)
            .Include(o => o.User)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
            query = query.Where(o => o.Status == status);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(o => o.OrderNumber.Contains(search) ||
                o.User.Email.Contains(search) ||
                (o.User.FirstName + " " + o.User.LastName).Contains(search));

        query = query.OrderByDescending(o => o.CreatedAt);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return ApiResponse<PagedResult<OrderSummaryDto>>.Ok(new PagedResult<OrderSummaryDto>
        {
            Items = items.Select(MapToSummary).ToList(),
            Page = page, PageSize = pageSize, TotalCount = total
        });
    }

    public async Task<ApiResponse<OrderDto>> UpdateStatusAsync(int orderId, UpdateOrderStatusRequest request)
    {
        var order = await _db.Orders.FindAsync(orderId);
        if (order == null) return ApiResponse<OrderDto>.Fail("Order not found.");

        order.Status = request.Status;
        order.UpdatedAt = DateTime.UtcNow;

        if (request.Status == "Shipped" && !order.ShippedAt.HasValue)
            order.ShippedAt = DateTime.UtcNow;
        if (request.Status == "Delivered" && !order.DeliveredAt.HasValue)
            order.DeliveredAt = DateTime.UtcNow;

        _db.OrderStatusHistories.Add(new OrderStatusHistory
        {
            OrderId = orderId,
            Status = request.Status,
            Note = request.Note,
            CreatedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return await GetOrderByIdAsync(orderId);
    }

    public async Task<ApiResponse<OrderDto>> CancelOrderAsync(int orderId, CancelOrderRequest request, int? userId = null)
    {
        var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId && (userId == null || o.UserId == userId));
        if (order == null) return ApiResponse<OrderDto>.Fail("Order not found.");

        if (order.Status is "Shipped" or "Delivered")
            return ApiResponse<OrderDto>.Fail("Cannot cancel an order that has already been shipped or delivered.");

        order.Status = "Cancelled";
        order.CancelReason = request.Reason;
        order.CancelledAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;

        _db.OrderStatusHistories.Add(new OrderStatusHistory
        {
            OrderId = orderId,
            Status = "Cancelled",
            Note = request.Reason,
            CreatedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return await GetOrderByIdAsync(orderId);
    }

    public async Task<ApiResponse<OrderDto>> SetTrackingNumberAsync(int orderId, SetTrackingRequest request)
    {
        var order = await _db.Orders.FindAsync(orderId);
        if (order == null) return ApiResponse<OrderDto>.Fail("Order not found.");
        order.TrackingNumber = request.TrackingNumber;
        order.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return await GetOrderByIdAsync(orderId);
    }

    private IQueryable<Order> LoadOrderQuery() =>
        _db.Orders
            .Include(o => o.User)
            .Include(o => o.ShippingAddress)
            .Include(o => o.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Images)
            .Include(o => o.Items).ThenInclude(i => i.ProductVariant)
            .Include(o => o.Payment)
            .Include(o => o.StatusHistory.OrderBy(h => h.CreatedAt));

    internal static OrderDto MapToDto(Order o) => new()
    {
        Id = o.Id,
        OrderNumber = o.OrderNumber,
        UserId = o.UserId,
        CustomerName = o.User != null ? $"{o.User.FirstName} {o.User.LastName}" : string.Empty,
        CustomerEmail = o.User?.Email ?? string.Empty,
        Status = o.Status,
        ShippingAddress = o.ShippingAddress != null ? MapAddressDto(o.ShippingAddress) : new(),
        SubTotal = o.SubTotal,
        ShippingCost = o.ShippingCost,
        TaxAmount = o.TaxAmount,
        DiscountAmount = o.DiscountAmount,
        TotalAmount = o.TotalAmount,
        CouponCode = o.CouponCode,
        Notes = o.Notes,
        TrackingNumber = o.TrackingNumber,
        ShippedAt = o.ShippedAt,
        DeliveredAt = o.DeliveredAt,
        CancelledAt = o.CancelledAt,
        CancelReason = o.CancelReason,
        CreatedAt = o.CreatedAt,
        Items = o.Items.Select(i => new OrderItemDto
        {
            Id = i.Id,
            ProductId = i.ProductId,
            ProductVariantId = i.ProductVariantId,
            ProductName = i.ProductName,
            VariantName = i.VariantName,
            SKU = i.SKU,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            TotalPrice = i.TotalPrice,
            ProductImageUrl = i.Product?.Images?.FirstOrDefault(img => img.IsPrimary)?.ImageUrl
                ?? i.Product?.Images?.FirstOrDefault()?.ImageUrl
        }).ToList(),
        Payment = o.Payment != null ? new PaymentInfoDto
        {
            Method = o.Payment.Method,
            Status = o.Payment.Status,
            Amount = o.Payment.Amount,
            Currency = o.Payment.Currency,
            TransactionId = o.Payment.TransactionId,
            CardLastFour = o.Payment.CardLastFour,
            CardBrand = o.Payment.CardBrand,
            PaidAt = o.Payment.PaidAt
        } : null,
        StatusHistory = o.StatusHistory.Select(h => new OrderStatusHistoryDto
        {
            Status = h.Status,
            Note = h.Note,
            CreatedAt = h.CreatedAt
        }).ToList()
    };

    private static OrderSummaryDto MapToSummary(Order o) => new()
    {
        Id = o.Id,
        OrderNumber = o.OrderNumber,
        Status = o.Status,
        TotalAmount = o.TotalAmount,
        ItemCount = o.Items.Sum(i => i.Quantity),
        ItemImages = o.Items
            .Select(i => i.Product?.Images?.FirstOrDefault(img => img.IsPrimary)?.ImageUrl
                ?? i.Product?.Images?.FirstOrDefault()?.ImageUrl)
            .Where(url => url != null)
            .Cast<string>()
            .Take(3)
            .ToList(),
        CreatedAt = o.CreatedAt,
        TrackingNumber = o.TrackingNumber
    };

    private static ShopForge.Shared.DTOs.Common.AddressDto MapAddressDto(Address a) => new()
    {
        Id = a.Id,
        Label = a.Label,
        FullName = a.FullName,
        PhoneNumber = a.PhoneNumber,
        Line1 = a.Line1,
        Line2 = a.Line2,
        City = a.City,
        State = a.State,
        PostalCode = a.PostalCode,
        Country = a.Country,
        IsDefault = a.IsDefault
    };
}
