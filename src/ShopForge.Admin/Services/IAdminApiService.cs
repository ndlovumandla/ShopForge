using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Auth;
using ShopForge.Shared.DTOs.Categories;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Orders;
using ShopForge.Shared.DTOs.Products;
using ShopForge.Shared.DTOs.Reports;
using Microsoft.AspNetCore.Components.Forms;

namespace ShopForge.Admin.Services;

public interface IAdminApiService
{
    void SetToken(string token);

    // Auth
    Task<ApiResponse<AuthResponse>?> LoginAsync(LoginRequest request);
    Task<ApiResponse<bool>?> LogoutAsync(string refreshToken);
    Task<ApiResponse<UserProfileDto>?> GetProfileAsync();
    Task<ApiResponse<UserProfileDto>?> UpdateProfileAsync(UpdateProfileRequest request);
    Task<ApiResponse<bool>?> ChangePasswordAsync(ChangePasswordRequest request);

    // Dashboard
    Task<ApiResponse<DashboardSummaryDto>?> GetDashboardAsync();

    // Products
    Task<ApiResponse<PagedResult<ProductSummaryDto>>?> GetProductsAsync(int page = 1, int pageSize = 20, string? search = null, int? categoryId = null);
    Task<ApiResponse<ProductDto>?> GetProductAsync(int id);
    Task<ApiResponse<ProductDto>?> CreateProductAsync(CreateProductRequest request);
    Task<ApiResponse<ProductDto>?> UpdateProductAsync(int id, UpdateProductRequest request);
    Task<ApiResponse<bool>?> DeleteProductAsync(int id);
    Task<ApiResponse<bool>?> AdjustStockAsync(int id, int adjustment, string reason);
    Task<ApiResponse<ProductImageUploadResult>?> UploadProductImageAsync(IBrowserFile file);

    // Categories
    Task<ApiResponse<List<CategoryDto>>?> GetCategoriesAsync();
    Task<ApiResponse<CategoryDto>?> CreateCategoryAsync(CreateCategoryRequest request);
    Task<ApiResponse<CategoryDto>?> UpdateCategoryAsync(int id, CreateCategoryRequest request);
    Task<ApiResponse<bool>?> DeleteCategoryAsync(int id);

    // Orders
    Task<ApiResponse<PagedResult<OrderSummaryDto>>?> GetOrdersAsync(int page = 1, int pageSize = 20, string? search = null, string? status = null, DateTime? from = null, DateTime? to = null);
    Task<ApiResponse<OrderDto>?> GetOrderAsync(int id);
    Task<ApiResponse<OrderDto>?> UpdateOrderStatusAsync(int id, UpdateOrderStatusRequest request);
    Task<ApiResponse<OrderDto>?> SetTrackingNumberAsync(int id, string trackingNumber);
    Task<ApiResponse<bool>?> RefundOrderAsync(int id);

    // Customers
    Task<ApiResponse<PagedResult<AdminUserDto>>?> GetCustomersAsync(int page = 1, int pageSize = 20, string? search = null);
    Task<ApiResponse<AdminUserDto>?> GetCustomerAsync(int id);
    Task<ApiResponse<AuthResponse>?> RegisterCustomerAsync(RegisterRequest request);
    Task<ApiResponse<bool>?> ToggleCustomerActiveAsync(int id);
    Task<ApiResponse<bool>?> ChangeCustomerRoleAsync(int id, string role);

    // Coupons
    Task<ApiResponse<List<CouponDto>>?> GetCouponsAsync();
    Task<ApiResponse<CouponDto>?> CreateCouponAsync(CreateCouponRequest request);
    Task<ApiResponse<CouponDto>?> UpdateCouponAsync(int id, CreateCouponRequest request);
    Task<ApiResponse<bool>?> DeleteCouponAsync(int id);

    // Reports
    Task<ApiResponse<List<SalesDataPointDto>>?> GetSalesReportAsync(DateTime from, DateTime to, string groupBy = "day");
    Task<ApiResponse<List<RevenueDataPoint>>?> GetRevenueByCategoryAsync(DateTime from, DateTime to);
    Task<ApiResponse<List<TopProductDto>>?> GetTopProductsAsync(DateTime from, DateTime to, string by = "revenue");
    Task<ApiResponse<List<InventoryReportItemDto>>?> GetInventoryReportAsync();

    // Banners
    Task<ApiResponse<List<BannerSlideDto>>?> GetBannersAsync();
    Task<ApiResponse<BannerSlideDto>?> CreateBannerAsync(BannerSlideDto banner);
    Task<ApiResponse<BannerSlideDto>?> UpdateBannerAsync(int id, BannerSlideDto banner);
    Task<ApiResponse<bool>?> DeleteBannerAsync(int id);

    // Settings
    Task<ApiResponse<List<AppSettingDto>>?> GetSettingsAsync();
    Task<ApiResponse<bool>?> UpdateSettingsAsync(UpdateSettingsRequest request);
}
