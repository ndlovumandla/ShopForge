using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Reviews;

namespace ShopForge.Api.Services.Interfaces;

public interface IReviewService
{
    Task<ApiResponse<PagedResult<ReviewDto>>> GetProductReviewsAsync(int productId, int page, int pageSize, bool approvedOnly = true);
    Task<ApiResponse<ProductRatingSummaryDto>> GetRatingSummaryAsync(int productId);
    Task<ApiResponse<ReviewDto>> CreateReviewAsync(int productId, int userId, CreateReviewRequest request);
    Task<ApiResponse<ReviewDto>> ApproveReviewAsync(int reviewId);
    Task<ApiResponse<bool>> DeleteReviewAsync(int reviewId);
    Task<ApiResponse<PagedResult<ReviewDto>>> GetPendingReviewsAsync(int page, int pageSize);
    Task<ApiResponse<bool>> MarkHelpfulAsync(int reviewId);
}
