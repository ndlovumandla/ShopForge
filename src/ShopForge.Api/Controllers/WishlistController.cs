using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlist;

    public WishlistController(IWishlistService wishlist) => _wishlist = wishlist;

    private int GetUserId() => int.Parse(User.FindFirstValue(AppConstants.JwtClaims.UserId)!);

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<ProductSummaryDto>>>> Get()
        => Ok(await _wishlist.GetWishlistAsync(GetUserId()));

    [HttpPost("{productId:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Add(int productId)
        => Ok(await _wishlist.AddToWishlistAsync(GetUserId(), productId));

    [HttpDelete("{productId:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Remove(int productId)
        => Ok(await _wishlist.RemoveFromWishlistAsync(GetUserId(), productId));

    [HttpGet("{productId:int}/check")]
    public async Task<ActionResult<ApiResponse<bool>>> Check(int productId)
        => Ok(await _wishlist.IsInWishlistAsync(GetUserId(), productId));
}
