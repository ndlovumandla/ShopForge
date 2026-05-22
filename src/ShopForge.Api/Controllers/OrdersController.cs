using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Orders;

namespace ShopForge.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("api-sensitive")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orders;
    private readonly IMemoryCache _cache;

    public OrdersController(IOrderService orders, IMemoryCache cache)
    {
        _orders = orders;
        _cache = cache;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(AppConstants.JwtClaims.UserId)!);

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<OrderSummaryDto>>>> GetMyOrders(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
        => Ok(await _orders.GetUserOrdersAsync(GetUserId(), page, pageSize));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> GetById(int id)
    {
        var result = await _orders.GetOrderByIdAsync(id, GetUserId());
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("number/{orderNumber}")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> GetByNumber(string orderNumber)
    {
        var result = await _orders.GetOrderByNumberAsync(orderNumber, GetUserId());
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<OrderDto>>> Create([FromBody] CreateOrderRequest request)
    {
        var userId = GetUserId();
        var idempotencyKey = GetIdempotencyKeyOrNull();

        if (!string.IsNullOrWhiteSpace(idempotencyKey))
        {
            var cacheKey = BuildIdempotencyCacheKey(userId, idempotencyKey, "create");
            if (_cache.TryGetValue<ApiResponse<OrderDto>>(cacheKey, out var cachedResult) && cachedResult is not null)
            {
                Response.Headers["X-Idempotent-Replay"] = "true";
                return CreatedAtAction(nameof(GetById), new { id = cachedResult.Data?.Id }, cachedResult);
            }

            var result = await _orders.CreateOrderAsync(userId, request);
            if (result.Success)
            {
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(AppConstants.Idempotency.CacheMinutes));
            }

            return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result) : BadRequest(result);
        }

        var uncachedResult = await _orders.CreateOrderAsync(userId, request);
        return uncachedResult.Success
            ? CreatedAtAction(nameof(GetById), new { id = uncachedResult.Data?.Id }, uncachedResult)
            : BadRequest(uncachedResult);
    }

    [HttpPost("{id:int}/cancel")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> Cancel(int id, [FromBody] CancelOrderRequest request)
    {
        var result = await _orders.CancelOrderAsync(id, request, GetUserId());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    private string? GetIdempotencyKeyOrNull()
    {
        if (!Request.Headers.TryGetValue(AppConstants.Idempotency.HeaderName, out var keyValues))
            return null;

        var key = keyValues.ToString().Trim();
        if (string.IsNullOrWhiteSpace(key))
            return null;

        if (key.Length > AppConstants.Idempotency.MaxKeyLength)
            return null;

        return key;
    }

    private static string BuildIdempotencyCacheKey(int userId, string idempotencyKey, string action)
        => $"idem:orders:{action}:u{userId}:{idempotencyKey}";
}
