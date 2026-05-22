using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;

namespace ShopForge.Database.SeedData;

public static class BrandSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = 1, Name = "TechPro", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 2, Name = "UrbanWear", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 3, Name = "HomeStyle", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 4, Name = "ActiveGear", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 5, Name = "ReadMore", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 6, Name = "PureLife", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 7, Name = "PlayZone", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 8, Name = "FreshMart", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 9, Name = "ProBrand", IsActive = true, CreatedAt = now, UpdatedAt = now },
            new Brand { Id = 10, Name = "ValueChoice", IsActive = true, CreatedAt = now, UpdatedAt = now }
        );
    }
}
