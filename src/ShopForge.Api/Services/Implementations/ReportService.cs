using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Reports;

namespace ShopForge.Api.Services.Implementations;

public class ReportService : IReportService
{
    private readonly ShopForgeDbContext _db;

    public ReportService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<DashboardSummaryDto>> GetDashboardSummaryAsync()
    {
        var today = DateTime.UtcNow.Date;
        var yesterday = today.AddDays(-1);
        var weekAgo = today.AddDays(-7);

        var todayOrders = await _db.Orders.Where(o => o.CreatedAt.Date == today && o.Status != "Cancelled").ToListAsync();
        var yesterdayOrders = await _db.Orders.Where(o => o.CreatedAt.Date == yesterday && o.Status != "Cancelled").ToListAsync();

        var recentOrders = await _db.Orders
            .Include(o => o.User)
            .Where(o => o.Status != "Cancelled")
            .OrderByDescending(o => o.CreatedAt)
            .Take(5)
            .ToListAsync();

        var lowStockProducts = await _db.Products
            .Where(p => p.IsActive && p.TrackInventory && p.StockQuantity <= p.LowStockThreshold)
            .OrderBy(p => p.StockQuantity)
            .Take(10)
            .ToListAsync();

        var newCustomers = await _db.Users
            .CountAsync(u => u.CreatedAt >= weekAgo && u.CreatedAt < today.AddDays(1));

        var pendingOrders = await _db.Orders.CountAsync(o => o.Status == "Pending");

        return ApiResponse<DashboardSummaryDto>.Ok(new DashboardSummaryDto
        {
            TodayRevenue = todayOrders.Sum(o => o.TotalAmount),
            YesterdayRevenue = yesterdayOrders.Sum(o => o.TotalAmount),
            TodayOrders = todayOrders.Count,
            YesterdayOrders = yesterdayOrders.Count,
            NewCustomersThisWeek = newCustomers,
            PendingOrders = pendingOrders,
            LowStockProducts = lowStockProducts.Count,
            RecentOrders = recentOrders.Select(o => new RecentOrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                CustomerName = o.User != null ? $"{o.User.FirstName} {o.User.LastName}" : "Unknown",
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                CreatedAt = o.CreatedAt
            }).ToList(),
            LowStockAlerts = lowStockProducts.Select(p => new LowStockItemDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                SKU = p.SKU,
                StockQuantity = p.StockQuantity,
                LowStockThreshold = p.LowStockThreshold
            }).ToList()
        });
    }

    public async Task<ApiResponse<List<SalesDataPointDto>>> GetSalesDataAsync(DateTime from, DateTime to, string groupBy = "day")
    {
        var orders = await _db.Orders
            .Include(o => o.User)
            .Where(o => o.CreatedAt >= from && o.CreatedAt <= to && o.Status != "Cancelled")
            .ToListAsync();

        var grouped = groupBy.ToLower() switch
        {
            "month" => orders.GroupBy(o => new DateTime(o.CreatedAt.Year, o.CreatedAt.Month, 1)),
            "week" => orders.GroupBy(o =>
            {
                var diff = (int)o.CreatedAt.DayOfWeek;
                return o.CreatedAt.Date.AddDays(-diff);
            }),
            _ => orders.GroupBy(o => o.CreatedAt.Date)
        };

        var newCustomersByDay = await _db.Users
            .Where(u => u.CreatedAt >= from && u.CreatedAt <= to)
            .ToListAsync();

        var result = grouped.OrderBy(g => g.Key).Select(g =>
        {
            var dayFrom = g.Key;
            var dayTo = groupBy.ToLower() == "month"
                ? dayFrom.AddMonths(1)
                : groupBy.ToLower() == "week"
                    ? dayFrom.AddDays(7)
                    : dayFrom.AddDays(1);

            return new SalesDataPointDto
            {
                Date = g.Key,
                OrderCount = g.Count(),
                Revenue = g.Sum(o => o.TotalAmount),
                AverageOrderValue = g.Any() ? g.Average(o => o.TotalAmount) : 0,
                NewCustomers = newCustomersByDay.Count(u => u.CreatedAt >= dayFrom && u.CreatedAt < dayTo)
            };
        }).ToList();

        return ApiResponse<List<SalesDataPointDto>>.Ok(result);
    }

    public async Task<ApiResponse<List<RevenueDataPoint>>> GetRevenueByCategory(DateTime from, DateTime to)
    {
        var orderItems = await _db.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Product).ThenInclude(p => p.Category)
            .Where(oi => oi.Order.CreatedAt >= from && oi.Order.CreatedAt <= to && oi.Order.Status != "Cancelled")
            .ToListAsync();

        var grouped = orderItems
            .GroupBy(oi => oi.Product?.Category?.Name ?? "Unknown")
            .ToList();

        var totalRevenue = grouped.Sum(g => g.Sum(oi => oi.TotalPrice));

        return ApiResponse<List<RevenueDataPoint>>.Ok(
            grouped.Select(g => new RevenueDataPoint
            {
                Category = g.Key,
                Revenue = g.Sum(oi => oi.TotalPrice),
                OrderCount = g.Select(oi => oi.OrderId).Distinct().Count(),
                ItemsSold = g.Sum(oi => oi.Quantity),
                Percentage = totalRevenue > 0 ? Math.Round((double)(g.Sum(oi => oi.TotalPrice) / totalRevenue) * 100, 2) : 0
            })
            .OrderByDescending(d => d.Revenue)
            .ToList()
        );
    }

    public async Task<ApiResponse<List<TopProductDto>>> GetTopProductsAsync(int count, DateTime from, DateTime to)
    {
        var items = await _db.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Product).ThenInclude(p => p.Category)
            .Include(oi => oi.Product).ThenInclude(p => p.Images)
            .Include(oi => oi.Product).ThenInclude(p => p.Reviews)
            .Where(oi => oi.Order.CreatedAt >= from && oi.Order.CreatedAt <= to && oi.Order.Status != "Cancelled")
            .ToListAsync();

        var grouped = items
            .GroupBy(oi => oi.ProductId)
            .OrderByDescending(g => g.Sum(oi => oi.Quantity))
            .Take(count)
            .Select((g, idx) =>
            {
                var product = g.First().Product;
                return new TopProductDto
                {
                    ProductId = g.Key,
                    ProductName = g.First().ProductName,
                    SKU = g.First().SKU,
                    CategoryName = product?.Category?.Name,
                    ImageUrl = product?.Images?.FirstOrDefault(i => i.IsPrimary)?.ImageUrl ?? product?.Images?.FirstOrDefault()?.ImageUrl,
                    QuantitySold = g.Sum(oi => oi.Quantity),
                    Revenue = g.Sum(oi => oi.TotalPrice),
                    AverageRating = product?.Reviews?.Any() == true ? product.Reviews.Average(r => (double)r.Rating) : 0,
                    Rank = idx + 1
                };
            })
            .ToList();

        return ApiResponse<List<TopProductDto>>.Ok(grouped);
    }

    public async Task<ApiResponse<List<InventoryReportItemDto>>> GetInventoryReportAsync()
    {
        var products = await _db.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive && p.TrackInventory)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();

        var result = products.Select(p => new InventoryReportItemDto
        {
            ProductId = p.Id,
            ProductName = p.Name,
            SKU = p.SKU,
            CategoryName = p.Category?.Name,
            StockQuantity = p.StockQuantity,
            LowStockThreshold = p.LowStockThreshold,
            StockStatus = p.StockQuantity == 0 ? "OutOfStock"
                : p.StockQuantity <= p.LowStockThreshold ? "LowStock"
                : "Healthy"
        }).ToList();

        return ApiResponse<List<InventoryReportItemDto>>.Ok(result);
    }

    public async Task<ApiResponse<List<OrderStatusDistributionDto>>> GetOrderStatusDistributionAsync(DateTime from, DateTime to)
    {
        var orders = await _db.Orders
            .Where(o => o.CreatedAt >= from && o.CreatedAt <= to)
            .ToListAsync();

        var total = orders.Count;
        var grouped = orders.GroupBy(o => o.Status).Select(g => new OrderStatusDistributionDto
        {
            Status = g.Key,
            Count = g.Count(),
            Percentage = total > 0 ? Math.Round((double)g.Count() / total * 100, 2) : 0
        }).OrderByDescending(d => d.Count).ToList();

        return ApiResponse<List<OrderStatusDistributionDto>>.Ok(grouped);
    }

    public async Task<ApiResponse<List<CouponUsageDto>>> GetCouponUsageReportAsync()
    {
        var coupons = await _db.Coupons.Include(c => c.Orders).ToListAsync();

        var result = coupons.Select(c => new CouponUsageDto
        {
            CouponId = c.Id,
            Code = c.Code,
            DiscountType = c.DiscountType,
            DiscountValue = c.DiscountValue,
            UsageCount = c.UsageCount,
            UsageLimit = c.UsageLimit,
            TotalDiscountGiven = c.Orders.Sum(o => o.DiscountAmount),
            IsActive = c.IsActive
        }).OrderByDescending(c => c.UsageCount).ToList();

        return ApiResponse<List<CouponUsageDto>>.Ok(result);
    }
}
