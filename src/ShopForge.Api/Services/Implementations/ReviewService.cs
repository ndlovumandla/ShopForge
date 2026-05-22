using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Reviews;

namespace ShopForge.Api.Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly ShopForgeDbContext _db;

    public ReviewService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<PagedResult<ReviewDto>>> GetProductReviewsAsync(int productId, int page, int pageSize, bool approvedOnly = true)
    {
        var query = _db.ProductReviews
            .Include(r => r.User)
            .Where(r => r.ProductId == productId);

        if (approvedOnly) query = query.Where(r => r.IsApproved);

        query = query.OrderByDescending(r => r.CreatedAt);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return ApiResponse<PagedResult<ReviewDto>>.Ok(new PagedResult<ReviewDto>
        {
            Items = items.Select(MapToDto).ToList(),
            Page = page, PageSize = pageSize, TotalCount = total
        });
    }

    public async Task<ApiResponse<ProductRatingSummaryDto>> GetRatingSummaryAsync(int productId)
    {
        var reviews = await _db.ProductReviews
            .Where(r => r.ProductId == productId && r.IsApproved)
            .ToListAsync();

        var summary = new ProductRatingSummaryDto
        {
            TotalReviews = reviews.Count,
            AverageRating = reviews.Any() ? reviews.Average(r => (double)r.Rating) : 0,
            RatingBreakdown = Enumerable.Range(1, 5)
                .ToDictionary(i => i, i => reviews.Count(r => r.Rating == i))
        };

        return ApiResponse<ProductRatingSummaryDto>.Ok(summary);
    }

    public async Task<ApiResponse<ReviewDto>> CreateReviewAsync(int productId, int userId, CreateReviewRequest request)
    {
        // Check if user already reviewed this product
        if (await _db.ProductReviews.AnyAsync(r => r.ProductId == productId && r.UserId == userId))
            return ApiResponse<ReviewDto>.Fail("You have already reviewed this product.");

        bool isVerified = false;
        if (request.OrderId.HasValue)
        {
            isVerified = await _db.OrderItems
                .Include(oi => oi.Order)
                .AnyAsync(oi => oi.ProductId == productId && oi.Order.UserId == userId && oi.OrderId == request.OrderId.Value);
        }
        else
        {
            isVerified = await _db.OrderItems
                .Include(oi => oi.Order)
                .AnyAsync(oi => oi.ProductId == productId && oi.Order.UserId == userId);
        }

        var review = new ProductReview
        {
            ProductId = productId,
            UserId = userId,
            OrderId = request.OrderId,
            Rating = (byte)Math.Clamp(request.Rating, 1, 5),
            Title = request.Title,
            Body = request.Body,
            IsVerifiedPurchase = isVerified,
            IsApproved = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.ProductReviews.Add(review);
        await _db.SaveChangesAsync();

        var user = await _db.Users.FindAsync(userId);
        review.User = user!;

        return ApiResponse<ReviewDto>.Ok(MapToDto(review), "Review submitted and awaiting approval.");
    }

    public async Task<ApiResponse<ReviewDto>> ApproveReviewAsync(int reviewId)
    {
        var review = await _db.ProductReviews.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == reviewId);
        if (review == null) return ApiResponse<ReviewDto>.Fail("Review not found.");

        review.IsApproved = true;
        review.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return ApiResponse<ReviewDto>.Ok(MapToDto(review));
    }

    public async Task<ApiResponse<bool>> DeleteReviewAsync(int reviewId)
    {
        var review = await _db.ProductReviews.FindAsync(reviewId);
        if (review == null) return ApiResponse<bool>.Fail("Review not found.");
        _db.ProductReviews.Remove(review);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<PagedResult<ReviewDto>>> GetPendingReviewsAsync(int page, int pageSize)
    {
        var query = _db.ProductReviews
            .Include(r => r.User)
            .Where(r => !r.IsApproved)
            .OrderByDescending(r => r.CreatedAt);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return ApiResponse<PagedResult<ReviewDto>>.Ok(new PagedResult<ReviewDto>
        {
            Items = items.Select(MapToDto).ToList(),
            Page = page, PageSize = pageSize, TotalCount = total
        });
    }

    public async Task<ApiResponse<bool>> MarkHelpfulAsync(int reviewId)
    {
        var review = await _db.ProductReviews.FindAsync(reviewId);
        if (review == null) return ApiResponse<bool>.Fail("Review not found.");
        review.HelpfulCount++;
        review.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    private static ReviewDto MapToDto(ProductReview r) => new()
    {
        Id = r.Id,
        ProductId = r.ProductId,
        UserId = r.UserId,
        UserName = r.User != null ? $"{r.User.FirstName} {r.User.LastName}" : "Anonymous",
        Rating = r.Rating,
        Title = r.Title,
        Body = r.Body,
        IsVerifiedPurchase = r.IsVerifiedPurchase,
        IsApproved = r.IsApproved,
        HelpfulCount = r.HelpfulCount,
        CreatedAt = r.CreatedAt
    };
}
