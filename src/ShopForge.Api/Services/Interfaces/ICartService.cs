using ShopForge.Shared.DTOs.Cart;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Interfaces;

public interface ICartService
{
    Task<ApiResponse<CartDto>> GetCartAsync(int? userId, string? sessionId);
    Task<ApiResponse<CartDto>> AddItemAsync(int? userId, string? sessionId, AddToCartRequest request);
    Task<ApiResponse<CartDto>> UpdateItemAsync(int? userId, string? sessionId, int itemId, UpdateCartItemRequest request);
    Task<ApiResponse<CartDto>> RemoveItemAsync(int? userId, string? sessionId, int itemId);
    Task<ApiResponse<bool>> ClearCartAsync(int? userId, string? sessionId);
    Task<ApiResponse<CartDto>> ApplyCouponAsync(int? userId, string? sessionId, string couponCode);
    Task<ApiResponse<CartDto>> RemoveCouponAsync(int? userId, string? sessionId);
    Task MergeCartAsync(int userId, string sessionId);
}
