using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Reviews;

namespace ShopForge.Api.Controllers;

[ApiController]
[Route("api/products/{productId:int}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviews;

    public ReviewsController(IReviewService reviews) => _reviews = reviews;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ReviewDto>>>> GetReviews(
        int productId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        => Ok(await _reviews.GetProductReviewsAsync(productId, page, pageSize));

    [HttpGet("summary")]
    public async Task<ActionResult<ApiResponse<ProductRatingSummaryDto>>> GetSummary(int productId)
        => Ok(await _reviews.GetRatingSummaryAsync(productId));

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ReviewDto>>> Create(int productId, [FromBody] CreateReviewRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(AppConstants.JwtClaims.UserId)!);
        var result = await _reviews.CreateReviewAsync(productId, userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpPost("{reviewId:int}/helpful")]
    public async Task<ActionResult<ApiResponse<bool>>> MarkHelpful(int productId, int reviewId)
        => Ok(await _reviews.MarkHelpfulAsync(reviewId));
}
