using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Orders;

namespace ShopForge.Api.Controllers;

[Authorize(Policy = "AdminOrManager")]
[ApiController]
[Route("api/admin/orders")]
public class AdminOrdersController : ControllerBase
{
    private readonly IOrderService _orders;

    public AdminOrdersController(IOrderService orders) => _orders = orders;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<OrderSummaryDto>>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? status = null,
        [FromQuery] string? search = null)
        => Ok(await _orders.GetAllOrdersAsync(page, pageSize, status, search));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> GetById(int id)
    {
        var result = await _orders.GetOrderByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> UpdateStatus(int id, [FromBody] UpdateOrderStatusRequest request)
    {
        var result = await _orders.UpdateStatusAsync(id, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:int}/tracking")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> SetTracking(int id, [FromBody] SetTrackingRequest request)
    {
        var result = await _orders.SetTrackingNumberAsync(id, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{id:int}/cancel")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> Cancel(int id, [FromBody] CancelOrderRequest request)
    {
        var result = await _orders.CancelOrderAsync(id, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
