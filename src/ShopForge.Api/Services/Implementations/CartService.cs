using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Cart;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Implementations;

public class CartService : ICartService
{
    private readonly ShopForgeDbContext _db;
    private static readonly ConcurrentDictionary<int, (string Code, decimal Discount)> _couponState = new();

    public CartService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<CartDto>> GetCartAsync(int? userId, string? sessionId)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId);
        return ApiResponse<CartDto>.Ok(MapToDto(cart));
    }

    public async Task<ApiResponse<CartDto>> AddItemAsync(int? userId, string? sessionId, AddToCartRequest request)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId);

        var product = await _db.Products
            .Include(p => p.Images)
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId && p.IsActive);

        if (product == null) return ApiResponse<CartDto>.Fail("Product not found.");

        decimal unitPrice = product.Price;
        int stock = product.StockQuantity;

        if (request.ProductVariantId.HasValue)
        {
            var variant = product.Variants.FirstOrDefault(v => v.Id == request.ProductVariantId.Value && v.IsActive);
            if (variant == null) return ApiResponse<CartDto>.Fail("Variant not found.");
            unitPrice = variant.Price;
            stock = variant.StockQuantity;
        }

        if (request.Quantity > stock) return ApiResponse<CartDto>.Fail("Insufficient stock.");

        var existing = cart.Items.FirstOrDefault(i =>
            i.ProductId == request.ProductId && i.ProductVariantId == request.ProductVariantId);

        if (existing != null)
        {
            existing.Quantity += request.Quantity;
            if (existing.Quantity > stock) existing.Quantity = stock;
            existing.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            _db.CartItems.Add(new CartItem
            {
                CartId = cart.Id,
                ProductId = request.ProductId,
                ProductVariantId = request.ProductVariantId,
                Quantity = request.Quantity,
                UnitPrice = unitPrice,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var updated = await LoadCartAsync(cart.Id);
        return ApiResponse<CartDto>.Ok(MapToDto(updated!));
    }

    public async Task<ApiResponse<CartDto>> UpdateItemAsync(int? userId, string? sessionId, int itemId, UpdateCartItemRequest request)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId);
        var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null) return ApiResponse<CartDto>.Fail("Cart item not found.");

        if (request.Quantity <= 0)
        {
            _db.CartItems.Remove(item);
        }
        else
        {
            item.Quantity = request.Quantity;
            item.UpdatedAt = DateTime.UtcNow;
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var updated = await LoadCartAsync(cart.Id);
        return ApiResponse<CartDto>.Ok(MapToDto(updated!));
    }

    public async Task<ApiResponse<CartDto>> RemoveItemAsync(int? userId, string? sessionId, int itemId)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId);
        var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _db.CartItems.Remove(item);
            cart.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        var updated = await LoadCartAsync(cart.Id);
        return ApiResponse<CartDto>.Ok(MapToDto(updated!));
    }

    public async Task<ApiResponse<bool>> ClearCartAsync(int? userId, string? sessionId)
    {
        var cart = await FindCartAsync(userId, sessionId);
        if (cart != null)
        {
            _db.CartItems.RemoveRange(cart.Items);
            _couponState.TryRemove(cart.Id, out _);
            cart.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<CartDto>> ApplyCouponAsync(int? userId, string? sessionId, string couponCode)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId);
        var cartDto = MapToDto(cart);

        var coupon = await _db.Coupons.FirstOrDefaultAsync(c =>
            c.Code.ToUpper() == couponCode.ToUpper() && c.IsActive);

        if (coupon == null) return ApiResponse<CartDto>.Fail("Coupon not found or inactive.");

        var now = DateTime.UtcNow;
        if (coupon.StartsAt.HasValue && now < coupon.StartsAt.Value)
            return ApiResponse<CartDto>.Fail("Coupon is not yet valid.");
        if (coupon.ExpiresAt.HasValue && now > coupon.ExpiresAt.Value)
            return ApiResponse<CartDto>.Fail("Coupon has expired.");
        if (coupon.UsageLimit.HasValue && coupon.UsageCount >= coupon.UsageLimit.Value)
            return ApiResponse<CartDto>.Fail("Coupon usage limit reached.");
        if (coupon.MinimumOrderAmount.HasValue && cartDto.SubTotal < coupon.MinimumOrderAmount.Value)
            return ApiResponse<CartDto>.Fail($"Minimum order amount is {coupon.MinimumOrderAmount:C}.");

        decimal discount = coupon.DiscountType == "Percentage"
            ? Math.Round(cartDto.SubTotal * (coupon.DiscountValue / 100), 2)
            : coupon.DiscountValue;

        if (coupon.MaximumDiscountAmount.HasValue && discount > coupon.MaximumDiscountAmount.Value)
            discount = coupon.MaximumDiscountAmount.Value;

        _couponState[cart.Id] = (coupon.Code, discount);

        cartDto.CouponCode = coupon.Code;
        cartDto.DiscountAmount = discount;

        return ApiResponse<CartDto>.Ok(cartDto);
    }

    public async Task<ApiResponse<CartDto>> RemoveCouponAsync(int? userId, string? sessionId)
    {
        var cart = await FindCartAsync(userId, sessionId);
        if (cart != null)
            _couponState.TryRemove(cart.Id, out _);

        var updated = cart != null ? await LoadCartAsync(cart.Id) : null;
        var dto = updated != null ? MapToDto(updated) : new CartDto();
        return ApiResponse<CartDto>.Ok(dto);
    }

    public async Task MergeCartAsync(int userId, string sessionId)
    {
        var sessionCart = await LoadCartAsync(sessionId: sessionId);
        if (sessionCart == null) return;

        var userCart = await GetOrCreateCartAsync(userId, null);

        foreach (var item in sessionCart.Items)
        {
            var existing = userCart.Items.FirstOrDefault(i =>
                i.ProductId == item.ProductId && i.ProductVariantId == item.ProductVariantId);
            if (existing != null)
                existing.Quantity += item.Quantity;
            else
                _db.CartItems.Add(new CartItem
                {
                    CartId = userCart.Id,
                    ProductId = item.ProductId,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
        }

        if (_couponState.TryRemove(sessionCart.Id, out var couponInfo))
            _couponState[userCart.Id] = couponInfo;

        _db.CartItems.RemoveRange(sessionCart.Items);
        _db.Carts.Remove(sessionCart);
        userCart.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
    }

    // Internal method used by OrderService to get coupon state
    public (string? Code, decimal Discount) GetCouponState(int cartId)
    {
        if (_couponState.TryGetValue(cartId, out var state))
            return (state.Code, state.Discount);
        return (null, 0);
    }

    private async Task<Cart> GetOrCreateCartAsync(int? userId, string? sessionId)
    {
        var cart = await FindCartAsync(userId, sessionId);
        if (cart != null) return cart;

        var newCart = new Cart
        {
            UserId = userId,
            SessionId = userId == null ? sessionId : null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _db.Carts.Add(newCart);
        await _db.SaveChangesAsync();

        return await LoadCartAsync(newCart.Id) ?? newCart;
    }

    private async Task<Cart?> FindCartAsync(int? userId, string? sessionId)
    {
        if (userId.HasValue)
            return await LoadCartAsync(userId: userId);
        if (!string.IsNullOrEmpty(sessionId))
            return await LoadCartAsync(sessionId: sessionId);
        return null;
    }

    private async Task<Cart?> LoadCartAsync(int? cartId = null, int? userId = null, string? sessionId = null)
    {
        var query = _db.Carts
            .Include(c => c.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Images)
            .Include(c => c.Items).ThenInclude(i => i.ProductVariant)
            .AsQueryable();

        if (cartId.HasValue) return await query.FirstOrDefaultAsync(c => c.Id == cartId.Value);
        if (userId.HasValue) return await query.FirstOrDefaultAsync(c => c.UserId == userId.Value);
        if (!string.IsNullOrEmpty(sessionId)) return await query.FirstOrDefaultAsync(c => c.SessionId == sessionId);

        return null;
    }

    private CartDto MapToDto(Cart cart)
    {
        var items = cart.Items.Select(i => new CartItemDto
        {
            Id = i.Id,
            ProductId = i.ProductId,
            ProductName = i.Product?.Name ?? string.Empty,
            ProductSlug = i.Product?.Slug ?? string.Empty,
            ProductImageUrl = i.Product?.Images?.FirstOrDefault(img => img.IsPrimary)?.ImageUrl
                ?? i.Product?.Images?.FirstOrDefault()?.ImageUrl,
            ProductVariantId = i.ProductVariantId,
            VariantName = i.ProductVariant?.Name,
            SKU = i.ProductVariant?.SKU ?? i.Product?.SKU ?? string.Empty,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            StockQuantity = i.ProductVariant?.StockQuantity ?? i.Product?.StockQuantity ?? 0
        }).ToList();

        var dto = new CartDto
        {
            Id = cart.Id,
            Items = items
        };

        if (_couponState.TryGetValue(cart.Id, out var couponInfo))
        {
            dto.CouponCode = couponInfo.Code;
            dto.DiscountAmount = couponInfo.Discount;
        }

        return dto;
    }
}
