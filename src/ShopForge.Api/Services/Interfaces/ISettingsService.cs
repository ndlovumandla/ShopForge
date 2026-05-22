using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Interfaces;

public class BrandDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Website { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateBrandRequest
{
    public string Name { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Website { get; set; }
    public bool IsActive { get; set; } = true;
}

public interface ISettingsService
{
    Task<ApiResponse<List<AppSettingDto>>> GetAllSettingsAsync();
    Task<ApiResponse<bool>> UpdateSettingsAsync(UpdateSettingsRequest request);
    Task<ApiResponse<List<ShippingMethodDto>>> GetShippingMethodsAsync(bool activeOnly = false);
    Task<ApiResponse<ShippingMethodDto>> UpsertShippingMethodAsync(ShippingMethodDto dto);
    Task<ApiResponse<bool>> DeleteShippingMethodAsync(int id);
    Task<ApiResponse<List<BannerSlideDto>>> GetBannersAsync(bool activeOnly = false);
    Task<ApiResponse<BannerSlideDto>> UpsertBannerAsync(BannerSlideDto dto);
    Task<ApiResponse<bool>> DeleteBannerAsync(int id);
    Task<ApiResponse<List<BrandDto>>> GetBrandsAsync(bool activeOnly = false);
    Task<ApiResponse<BrandDto>> UpsertBrandAsync(CreateBrandRequest dto);
    Task<ApiResponse<BrandDto>> UpdateBrandAsync(int id, CreateBrandRequest dto);
    Task<ApiResponse<bool>> DeleteBrandAsync(int id);
}
