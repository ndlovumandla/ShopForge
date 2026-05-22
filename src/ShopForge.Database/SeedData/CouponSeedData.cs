using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;

namespace ShopForge.Database.SeedData;

public static class CouponSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var coupons = new List<Coupon>();

        for (var i = 1; i <= 10; i++)
        {
            var isPercentage = i % 3 != 0;
            var type = isPercentage ? "Percentage" : "FixedAmount";
            var value = isPercentage ? (5m + i) : (20m * i);

            coupons.Add(new Coupon
            {
                Id = i,
                Code = $"DUMMY{i:00}",
                Description = $"Demo coupon {i:00}",
                DiscountType = type,
                DiscountValue = value,
                MinimumOrderAmount = 100m * i,
                UsageLimit = 100 * i,
                UsageCount = 0,
                ExpiresAt = now.AddMonths(6 + i),
                IsActive = true,
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        modelBuilder.Entity<Coupon>().HasData(coupons);
    }
}
