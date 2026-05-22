using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Implementations;

public class CouponService : ICouponService
{
    private readonly ShopForgeDbContext _db;

    public CouponService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<CouponValidationResult>> ValidateCouponAsync(string code, decimal cartTotal)
    {
        var coupon = await _db.Coupons.FirstOrDefaultAsync(c =>
            c.Code.ToUpper() == code.ToUpper() && c.IsActive);

        if (coupon == null)
            return ApiResponse<CouponValidationResult>.Ok(new CouponValidationResult { IsValid = false, Message = "Coupon not found." });

        var now = DateTime.UtcNow;
        if (coupon.StartsAt.HasValue && now < coupon.StartsAt.Value)
            return ApiResponse<CouponValidationResult>.Ok(new CouponValidationResult { IsValid = false, Message = "Coupon not yet valid." });

        if (coupon.ExpiresAt.HasValue && now > coupon.ExpiresAt.Value)
            return ApiResponse<CouponValidationResult>.Ok(new CouponValidationResult { IsValid = false, Message = "Coupon has expired." });

        if (coupon.UsageLimit.HasValue && coupon.UsageCount >= coupon.UsageLimit.Value)
            return ApiResponse<CouponValidationResult>.Ok(new CouponValidationResult { IsValid = false, Message = "Coupon usage limit reached." });

        if (coupon.MinimumOrderAmount.HasValue && cartTotal < coupon.MinimumOrderAmount.Value)
            return ApiResponse<CouponValidationResult>.Ok(new CouponValidationResult
            {
                IsValid = false,
                Message = $"Minimum order amount is {coupon.MinimumOrderAmount:C}."
            });

        decimal discount = coupon.DiscountType == "Percentage"
            ? Math.Round(cartTotal * (coupon.DiscountValue / 100), 2)
            : coupon.DiscountValue;

        if (coupon.MaximumDiscountAmount.HasValue && discount > coupon.MaximumDiscountAmount.Value)
            discount = coupon.MaximumDiscountAmount.Value;

        return ApiResponse<CouponValidationResult>.Ok(new CouponValidationResult
        {
            IsValid = true,
            Message = "Coupon applied.",
            DiscountAmount = discount,
            DiscountType = coupon.DiscountType
        });
    }

    public async Task<ApiResponse<List<CouponDto>>> GetAllCouponsAsync()
    {
        var coupons = await _db.Coupons.OrderByDescending(c => c.CreatedAt).ToListAsync();
        return ApiResponse<List<CouponDto>>.Ok(coupons.Select(MapToDto).ToList());
    }

    public async Task<ApiResponse<CouponDto>> GetCouponByIdAsync(int id)
    {
        var coupon = await _db.Coupons.FindAsync(id);
        if (coupon == null) return ApiResponse<CouponDto>.Fail("Coupon not found.");
        return ApiResponse<CouponDto>.Ok(MapToDto(coupon));
    }

    public async Task<ApiResponse<CouponDto>> CreateCouponAsync(CreateCouponRequest request)
    {
        if (await _db.Coupons.AnyAsync(c => c.Code.ToUpper() == request.Code.ToUpper()))
            return ApiResponse<CouponDto>.Fail("A coupon with this code already exists.");

        var coupon = new Coupon
        {
            Code = request.Code.ToUpper(),
            Description = request.Description,
            DiscountType = request.DiscountType,
            DiscountValue = request.DiscountValue,
            MinimumOrderAmount = request.MinimumOrderAmount,
            MaximumDiscountAmount = request.MaximumDiscountAmount,
            UsageLimit = request.UsageLimit,
            StartsAt = request.StartsAt,
            ExpiresAt = request.ExpiresAt,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Coupons.Add(coupon);
        await _db.SaveChangesAsync();
        return ApiResponse<CouponDto>.Ok(MapToDto(coupon));
    }

    public async Task<ApiResponse<CouponDto>> UpdateCouponAsync(int id, CreateCouponRequest request)
    {
        var coupon = await _db.Coupons.FindAsync(id);
        if (coupon == null) return ApiResponse<CouponDto>.Fail("Coupon not found.");

        if (await _db.Coupons.AnyAsync(c => c.Code.ToUpper() == request.Code.ToUpper() && c.Id != id))
            return ApiResponse<CouponDto>.Fail("A coupon with this code already exists.");

        coupon.Code = request.Code.ToUpper();
        coupon.Description = request.Description;
        coupon.DiscountType = request.DiscountType;
        coupon.DiscountValue = request.DiscountValue;
        coupon.MinimumOrderAmount = request.MinimumOrderAmount;
        coupon.MaximumDiscountAmount = request.MaximumDiscountAmount;
        coupon.UsageLimit = request.UsageLimit;
        coupon.StartsAt = request.StartsAt;
        coupon.ExpiresAt = request.ExpiresAt;
        coupon.IsActive = request.IsActive;
        coupon.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return ApiResponse<CouponDto>.Ok(MapToDto(coupon));
    }

    public async Task<ApiResponse<bool>> DeleteCouponAsync(int id)
    {
        var coupon = await _db.Coupons.FindAsync(id);
        if (coupon == null) return ApiResponse<bool>.Fail("Coupon not found.");
        _db.Coupons.Remove(coupon);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<bool>> ToggleActiveAsync(int id)
    {
        var coupon = await _db.Coupons.FindAsync(id);
        if (coupon == null) return ApiResponse<bool>.Fail("Coupon not found.");
        coupon.IsActive = !coupon.IsActive;
        coupon.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(coupon.IsActive);
    }

    private static CouponDto MapToDto(Coupon c) => new()
    {
        Id = c.Id,
        Code = c.Code,
        Description = c.Description,
        DiscountType = c.DiscountType,
        DiscountValue = c.DiscountValue,
        MinimumOrderAmount = c.MinimumOrderAmount,
        MaximumDiscountAmount = c.MaximumDiscountAmount,
        UsageLimit = c.UsageLimit,
        UsageCount = c.UsageCount,
        StartsAt = c.StartsAt,
        ExpiresAt = c.ExpiresAt,
        IsActive = c.IsActive,
        CreatedAt = c.CreatedAt
    };
}
