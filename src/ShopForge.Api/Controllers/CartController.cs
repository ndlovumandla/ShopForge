using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Cart;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cart;

    public CartController(ICartService cart) => _cart = cart;

    private (int? UserId, string? SessionId) GetCartIdentity()
    {
        var userIdStr = User.FindFirstValue(AppConstants.JwtClaims.UserId);
        if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out var uid))
            return (uid, null);
        var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault()
            ?? Request.Cookies["session_id"];
        return (null, sessionId);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<CartDto>>> Get()
    {
        var (userId, sessionId) = GetCartIdentity();
        return Ok(await _cart.GetCartAsync(userId, sessionId));
    }

    [HttpPost("items")]
    public async Task<ActionResult<ApiResponse<CartDto>>> AddItem([FromBody] AddToCartRequest request)
    {
        var (userId, sessionId) = GetCartIdentity();
        var result = await _cart.AddItemAsync(userId, sessionId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("items/{itemId:int}")]
    public async Task<ActionResult<ApiResponse<CartDto>>> UpdateItem(int itemId, [FromBody] UpdateCartItemRequest request)
    {
        var (userId, sessionId) = GetCartIdentity();
        var result = await _cart.UpdateItemAsync(userId, sessionId, itemId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("items/{itemId:int}")]
    public async Task<ActionResult<ApiResponse<CartDto>>> RemoveItem(int itemId)
    {
        var (userId, sessionId) = GetCartIdentity();
        return Ok(await _cart.RemoveItemAsync(userId, sessionId, itemId));
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResponse<bool>>> Clear()
    {
        var (userId, sessionId) = GetCartIdentity();
        return Ok(await _cart.ClearCartAsync(userId, sessionId));
    }

    [HttpPost("coupon")]
    public async Task<ActionResult<ApiResponse<CartDto>>> ApplyCoupon([FromBody] ApplyCouponRequest request)
    {
        var (userId, sessionId) = GetCartIdentity();
        var result = await _cart.ApplyCouponAsync(userId, sessionId, request.CouponCode);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("coupon")]
    public async Task<ActionResult<ApiResponse<CartDto>>> RemoveCoupon()
    {
        var (userId, sessionId) = GetCartIdentity();
        return Ok(await _cart.RemoveCouponAsync(userId, sessionId));
    }
}
