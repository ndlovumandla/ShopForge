using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Implementations;

public class SettingsService : ISettingsService
{
    private readonly ShopForgeDbContext _db;

    public SettingsService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<List<AppSettingDto>>> GetAllSettingsAsync()
    {
        var settings = await _db.AppSettings.OrderBy(s => s.Key).ToListAsync();
        return ApiResponse<List<AppSettingDto>>.Ok(settings.Select(s => new AppSettingDto
        {
            Key = s.Key,
            Value = s.Value,
            Description = s.Description
        }).ToList());
    }

    public async Task<ApiResponse<bool>> UpdateSettingsAsync(UpdateSettingsRequest request)
    {
        foreach (var (key, value) in request.Settings)
        {
            var setting = await _db.AppSettings.FirstOrDefaultAsync(s => s.Key == key);
            if (setting != null)
            {
                setting.Value = value;
                setting.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                _db.AppSettings.Add(new AppSetting
                {
                    Key = key,
                    Value = value,
                    UpdatedAt = DateTime.UtcNow
                });
            }
        }
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<List<ShippingMethodDto>>> GetShippingMethodsAsync(bool activeOnly = false)
    {
        var query = _db.ShippingMethods.AsQueryable();
        if (activeOnly) query = query.Where(s => s.IsActive);
        var items = await query.OrderBy(s => s.Cost).ToListAsync();
        return ApiResponse<List<ShippingMethodDto>>.Ok(items.Select(MapShippingDto).ToList());
    }

    public async Task<ApiResponse<ShippingMethodDto>> UpsertShippingMethodAsync(ShippingMethodDto dto)
    {
        ShippingMethod method;
        if (dto.Id > 0)
        {
            method = await _db.ShippingMethods.FindAsync(dto.Id) ?? new ShippingMethod();
            if (method.Id == 0) _db.ShippingMethods.Add(method);
        }
        else
        {
            method = new ShippingMethod();
            _db.ShippingMethods.Add(method);
        }

        method.Name = dto.Name;
        method.Description = dto.Description;
        method.Cost = dto.Cost;
        method.EstimatedDaysMin = dto.EstimatedDaysMin;
        method.EstimatedDaysMax = dto.EstimatedDaysMax;
        method.IsActive = dto.IsActive;
        method.FreeShippingThreshold = dto.FreeShippingThreshold;

        await _db.SaveChangesAsync();
        return ApiResponse<ShippingMethodDto>.Ok(MapShippingDto(method));
    }

    public async Task<ApiResponse<bool>> DeleteShippingMethodAsync(int id)
    {
        var method = await _db.ShippingMethods.FindAsync(id);
        if (method == null) return ApiResponse<bool>.Fail("Shipping method not found.");
        _db.ShippingMethods.Remove(method);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<List<BannerSlideDto>>> GetBannersAsync(bool activeOnly = false)
    {
        var query = _db.BannerSlides.AsQueryable();
        if (activeOnly)
        {
            var now = DateTime.UtcNow;
            query = query.Where(b => b.IsActive &&
                (!b.StartsAt.HasValue || b.StartsAt <= now) &&
                (!b.ExpiresAt.HasValue || b.ExpiresAt >= now));
        }
        var items = await query.OrderBy(b => b.DisplayOrder).ToListAsync();
        return ApiResponse<List<BannerSlideDto>>.Ok(items.Select(MapBannerDto).ToList());
    }

    public async Task<ApiResponse<BannerSlideDto>> UpsertBannerAsync(BannerSlideDto dto)
    {
        BannerSlide banner;
        if (dto.Id > 0)
        {
            banner = await _db.BannerSlides.FindAsync(dto.Id) ?? new BannerSlide();
            if (banner.Id == 0) _db.BannerSlides.Add(banner);
        }
        else
        {
            banner = new BannerSlide();
            _db.BannerSlides.Add(banner);
        }

        banner.Title = dto.Title;
        banner.SubTitle = dto.SubTitle;
        banner.ImageUrl = dto.ImageUrl;
        banner.LinkUrl = dto.LinkUrl;
        banner.ButtonText = dto.ButtonText;
        banner.DisplayOrder = dto.DisplayOrder;
        banner.IsActive = dto.IsActive;
        banner.StartsAt = dto.StartsAt;
        banner.ExpiresAt = dto.ExpiresAt;

        await _db.SaveChangesAsync();
        return ApiResponse<BannerSlideDto>.Ok(MapBannerDto(banner));
    }

    public async Task<ApiResponse<bool>> DeleteBannerAsync(int id)
    {
        var banner = await _db.BannerSlides.FindAsync(id);
        if (banner == null) return ApiResponse<bool>.Fail("Banner not found.");
        _db.BannerSlides.Remove(banner);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<List<BrandDto>>> GetBrandsAsync(bool activeOnly = false)
    {
        var query = _db.Brands.AsQueryable();
        if (activeOnly) query = query.Where(b => b.IsActive);
        var brands = await query.OrderBy(b => b.Name).ToListAsync();
        return ApiResponse<List<BrandDto>>.Ok(brands.Select(MapBrandDto).ToList());
    }

    public async Task<ApiResponse<BrandDto>> UpsertBrandAsync(CreateBrandRequest dto)
    {
        var brand = new ShopForge.Database.Entities.Brand
        {
            Name = dto.Name,
            LogoUrl = dto.LogoUrl,
            Website = dto.Website,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _db.Brands.Add(brand);
        await _db.SaveChangesAsync();
        return ApiResponse<BrandDto>.Ok(MapBrandDto(brand));
    }

    public async Task<ApiResponse<BrandDto>> UpdateBrandAsync(int id, CreateBrandRequest dto)
    {
        var brand = await _db.Brands.FindAsync(id);
        if (brand == null) return ApiResponse<BrandDto>.Fail("Brand not found.");

        brand.Name = dto.Name;
        brand.LogoUrl = dto.LogoUrl;
        brand.Website = dto.Website;
        brand.IsActive = dto.IsActive;
        brand.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return ApiResponse<BrandDto>.Ok(MapBrandDto(brand));
    }

    public async Task<ApiResponse<bool>> DeleteBrandAsync(int id)
    {
        var brand = await _db.Brands.FindAsync(id);
        if (brand == null) return ApiResponse<bool>.Fail("Brand not found.");
        _db.Brands.Remove(brand);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    private static ShippingMethodDto MapShippingDto(ShippingMethod s) => new()
    {
        Id = s.Id,
        Name = s.Name,
        Description = s.Description,
        Cost = s.Cost,
        EstimatedDaysMin = s.EstimatedDaysMin,
        EstimatedDaysMax = s.EstimatedDaysMax,
        IsActive = s.IsActive,
        FreeShippingThreshold = s.FreeShippingThreshold
    };

    private static BannerSlideDto MapBannerDto(BannerSlide b) => new()
    {
        Id = b.Id,
        Title = b.Title,
        SubTitle = b.SubTitle,
        ImageUrl = b.ImageUrl,
        LinkUrl = b.LinkUrl,
        ButtonText = b.ButtonText,
        DisplayOrder = b.DisplayOrder,
        IsActive = b.IsActive,
        StartsAt = b.StartsAt,
        ExpiresAt = b.ExpiresAt
    };

    private static BrandDto MapBrandDto(ShopForge.Database.Entities.Brand b) => new()
    {
        Id = b.Id,
        Name = b.Name,
        LogoUrl = b.LogoUrl,
        Website = b.Website,
        IsActive = b.IsActive,
        CreatedAt = b.CreatedAt
    };
}
