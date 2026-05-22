namespace ShopForge.Shared.DTOs.Reports;

public class DashboardSummaryDto
{
    public decimal TodayRevenue { get; set; }
    public decimal YesterdayRevenue { get; set; }
    public int TodayOrders { get; set; }
    public int YesterdayOrders { get; set; }
    public int NewCustomersThisWeek { get; set; }
    public int PendingOrders { get; set; }
    public int LowStockProducts { get; set; }
    public List<RecentOrderDto> RecentOrders { get; set; } = new();
    public List<LowStockItemDto> LowStockAlerts { get; set; } = new();
}

public class RecentOrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class LowStockItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; }
}

public class SalesDataPointDto
{
    public DateTime Date { get; set; }
    public int OrderCount { get; set; }
    public decimal Revenue { get; set; }
    public decimal AverageOrderValue { get; set; }
    public int NewCustomers { get; set; }
}

public class RevenueDataPoint
{
    public string Category { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public int OrderCount { get; set; }
    public int ItemsSold { get; set; }
    public double Percentage { get; set; }
}

public class TopProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
    public double AverageRating { get; set; }
    public int Rank { get; set; }
}

public class InventoryReportItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; }
    public string StockStatus { get; set; } = string.Empty; // OutOfStock | LowStock | Healthy
}

public class OrderStatusDistributionDto
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}

public class CouponUsageDto
{
    public int CouponId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string DiscountType { get; set; } = string.Empty;
    public decimal DiscountValue { get; set; }
    public int UsageCount { get; set; }
    public int? UsageLimit { get; set; }
    public decimal TotalDiscountGiven { get; set; }
    public bool IsActive { get; set; }
}
