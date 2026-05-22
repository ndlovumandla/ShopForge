using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Payments;

namespace ShopForge.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("api-sensitive")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _payments;
    private readonly IMemoryCache _cache;

    public PaymentsController(IPaymentService payments, IMemoryCache cache)
    {
        _payments = payments;
        _cache = cache;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(AppConstants.JwtClaims.UserId)!);

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PaymentReceiptDto>>> Process([FromBody] ProcessPaymentRequest request)
    {
        var userId = GetUserId();
        var idempotencyKey = GetIdempotencyKeyOrNull();

        if (!string.IsNullOrWhiteSpace(idempotencyKey))
        {
            var cacheKey = BuildIdempotencyCacheKey(userId, idempotencyKey, "process");
            if (_cache.TryGetValue<ApiResponse<PaymentReceiptDto>>(cacheKey, out var cachedResult) && cachedResult is not null)
            {
                Response.Headers["X-Idempotent-Replay"] = "true";
                return Ok(cachedResult);
            }

            var result = await _payments.ProcessPaymentAsync(request, userId);
            if (result.Success)
            {
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(AppConstants.Idempotency.CacheMinutes));
            }

            return result.Success ? Ok(result) : BadRequest(result);
        }

        var uncachedResult = await _payments.ProcessPaymentAsync(request, userId);
        return uncachedResult.Success ? Ok(uncachedResult) : BadRequest(uncachedResult);
    }

    [HttpGet("order/{orderId:int}")]
    public async Task<ActionResult<ApiResponse<PaymentDto>>> GetByOrder(int orderId)
    {
        var result = await _payments.GetPaymentByOrderIdAsync(orderId);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [Authorize(Policy = "AdminOrManager")]
    [HttpPost("{orderId:int}/refund")]
    public async Task<ActionResult<ApiResponse<bool>>> Refund(int orderId)
    {
        var result = await _payments.RefundPaymentAsync(orderId);
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
        => $"idem:payments:{action}:u{userId}:{idempotencyKey}";
}
