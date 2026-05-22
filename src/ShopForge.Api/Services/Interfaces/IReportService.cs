using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Reports;

namespace ShopForge.Api.Services.Interfaces;

public interface IReportService
{
    Task<ApiResponse<DashboardSummaryDto>> GetDashboardSummaryAsync();
    Task<ApiResponse<List<SalesDataPointDto>>> GetSalesDataAsync(DateTime from, DateTime to, string groupBy = "day");
    Task<ApiResponse<List<RevenueDataPoint>>> GetRevenueByCategory(DateTime from, DateTime to);
    Task<ApiResponse<List<TopProductDto>>> GetTopProductsAsync(int count, DateTime from, DateTime to);
    Task<ApiResponse<List<InventoryReportItemDto>>> GetInventoryReportAsync();
    Task<ApiResponse<List<OrderStatusDistributionDto>>> GetOrderStatusDistributionAsync(DateTime from, DateTime to);
    Task<ApiResponse<List<CouponUsageDto>>> GetCouponUsageReportAsync();
}
