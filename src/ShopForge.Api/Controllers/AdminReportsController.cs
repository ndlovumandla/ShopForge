using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Reports;

namespace ShopForge.Api.Controllers;

[Authorize(Policy = "AdminOrManager")]
[ApiController]
[Route("api/admin/reports")]
public class AdminReportsController : ControllerBase
{
    private readonly IReportService _reports;

    public AdminReportsController(IReportService reports) => _reports = reports;

    [HttpGet("dashboard")]
    public async Task<ActionResult<ApiResponse<DashboardSummaryDto>>> Dashboard()
        => Ok(await _reports.GetDashboardSummaryAsync());

    [HttpGet("sales")]
    public async Task<ActionResult<ApiResponse<List<SalesDataPointDto>>>> Sales(
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null,
        [FromQuery] string groupBy = "day")
    {
        var fromDate = from ?? DateTime.UtcNow.AddDays(-30);
        var toDate = to ?? DateTime.UtcNow;
        return Ok(await _reports.GetSalesDataAsync(fromDate, toDate, groupBy));
    }

    [HttpGet("revenue-by-category")]
    public async Task<ActionResult<ApiResponse<List<RevenueDataPoint>>>> RevenueByCategory(
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null)
    {
        var fromDate = from ?? DateTime.UtcNow.AddDays(-30);
        var toDate = to ?? DateTime.UtcNow;
        return Ok(await _reports.GetRevenueByCategory(fromDate, toDate));
    }

    [HttpGet("top-products")]
    public async Task<ActionResult<ApiResponse<List<TopProductDto>>>> TopProducts(
        [FromQuery] int count = 10,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null)
    {
        var fromDate = from ?? DateTime.UtcNow.AddDays(-30);
        var toDate = to ?? DateTime.UtcNow;
        return Ok(await _reports.GetTopProductsAsync(count, fromDate, toDate));
    }

    [HttpGet("inventory")]
    public async Task<ActionResult<ApiResponse<List<InventoryReportItemDto>>>> Inventory()
        => Ok(await _reports.GetInventoryReportAsync());

    [HttpGet("order-status")]
    public async Task<ActionResult<ApiResponse<List<OrderStatusDistributionDto>>>> OrderStatus(
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null)
    {
        var fromDate = from ?? DateTime.UtcNow.AddDays(-30);
        var toDate = to ?? DateTime.UtcNow;
        return Ok(await _reports.GetOrderStatusDistributionAsync(fromDate, toDate));
    }

    [HttpGet("coupon-usage")]
    public async Task<ActionResult<ApiResponse<List<CouponUsageDto>>>> CouponUsage()
        => Ok(await _reports.GetCouponUsageReportAsync());
}
