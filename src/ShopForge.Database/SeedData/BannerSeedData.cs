using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;

namespace ShopForge.Database.SeedData;

public static class BannerSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<BannerSlide>().HasData(
            new BannerSlide { Id = 1, Title = "Summer Sale", SubTitle = "Up to 50% Off!", ImageUrl = "https://picsum.photos/seed/banner1/1200/400", LinkUrl = "/categories/electronics", ButtonText = "Shop Now", DisplayOrder = 1, IsActive = true, CreatedAt = now },
            new BannerSlide { Id = 2, Title = "New Arrivals in Clothing", SubTitle = "Fresh styles just landed", ImageUrl = "https://picsum.photos/seed/banner2/1200/400", LinkUrl = "/categories/clothing", ButtonText = "Explore Now", DisplayOrder = 2, IsActive = true, CreatedAt = now },
            new BannerSlide { Id = 3, Title = "Free Shipping", SubTitle = "On orders over R999", ImageUrl = "https://picsum.photos/seed/banner3/1200/400", LinkUrl = "/shop", ButtonText = "Start Shopping", DisplayOrder = 3, IsActive = true, CreatedAt = now }
        );
    }
}
