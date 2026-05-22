using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Api.Services.Interfaces;

public interface IWishlistService
{
    Task<ApiResponse<List<ProductSummaryDto>>> GetWishlistAsync(int userId);
    Task<ApiResponse<bool>> AddToWishlistAsync(int userId, int productId);
    Task<ApiResponse<bool>> RemoveFromWishlistAsync(int userId, int productId);
    Task<ApiResponse<bool>> IsInWishlistAsync(int userId, int productId);
}
