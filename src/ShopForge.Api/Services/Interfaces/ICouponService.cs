using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Interfaces;

public interface ICouponService
{
    Task<ApiResponse<CouponValidationResult>> ValidateCouponAsync(string code, decimal cartTotal);
    Task<ApiResponse<List<CouponDto>>> GetAllCouponsAsync();
    Task<ApiResponse<CouponDto>> GetCouponByIdAsync(int id);
    Task<ApiResponse<CouponDto>> CreateCouponAsync(CreateCouponRequest request);
    Task<ApiResponse<CouponDto>> UpdateCouponAsync(int id, CreateCouponRequest request);
    Task<ApiResponse<bool>> DeleteCouponAsync(int id);
    Task<ApiResponse<bool>> ToggleActiveAsync(int id);
}
