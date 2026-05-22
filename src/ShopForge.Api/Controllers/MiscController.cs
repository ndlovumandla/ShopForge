using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MiscController : ControllerBase
{
    private readonly ISettingsService _settings;
    private readonly ICouponService _coupons;

    public MiscController(ISettingsService settings, ICouponService coupons)
    {
        _settings = settings;
        _coupons = coupons;
    }

    [HttpGet("banners")]
    public async Task<ActionResult<ApiResponse<List<BannerSlideDto>>>> GetBanners()
        => Ok(await _settings.GetBannersAsync(activeOnly: true));

    [HttpGet("shipping-methods")]
    public async Task<ActionResult<ApiResponse<List<ShippingMethodDto>>>> GetShippingMethods()
        => Ok(await _settings.GetShippingMethodsAsync(activeOnly: true));

    [HttpGet("brands")]
    public async Task<ActionResult<ApiResponse<List<BrandDto>>>> GetBrands()
        => Ok(await _settings.GetBrandsAsync(activeOnly: true));

    [HttpPost("validate-coupon")]
    public async Task<ActionResult<ApiResponse<CouponValidationResult>>> ValidateCoupon([FromBody] ValidateCouponRequest request)
    {
        var result = await _coupons.ValidateCouponAsync(request.CouponCode, request.CartTotal);
        return Ok(result);
    }

    [HttpGet("admin/reviews")]
    public async Task<ActionResult<ApiResponse<PagedResult<ShopForge.Shared.DTOs.Reviews.ReviewDto>>>> GetPendingReviews(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromServices] IReviewService reviewService = null!)
        => Ok(await reviewService.GetPendingReviewsAsync(page, pageSize));

    [HttpPut("admin/reviews/{id:int}/approve")]
    public async Task<ActionResult<ApiResponse<ShopForge.Shared.DTOs.Reviews.ReviewDto>>> ApproveReview(
        int id,
        [FromServices] IReviewService reviewService = null!)
    {
        var result = await reviewService.ApproveReviewAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("admin/reviews/{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteReview(
        int id,
        [FromServices] IReviewService reviewService = null!)
    {
        var result = await reviewService.DeleteReviewAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
