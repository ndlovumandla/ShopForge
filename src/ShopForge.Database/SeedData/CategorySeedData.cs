using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;

namespace ShopForge.Database.SeedData;

public static class CategorySeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics", Slug = "electronics", Description = "Electronic devices and accessories", IsActive = true, DisplayOrder = 1, CreatedAt = now, UpdatedAt = now },
            new Category { Id = 2, Name = "Clothing & Apparel", Slug = "clothing", Description = "Fashion clothing and apparel", IsActive = true, DisplayOrder = 2, CreatedAt = now, UpdatedAt = now },
            new Category { Id = 3, Name = "Home & Garden", Slug = "home-garden", Description = "Home decor and garden products", IsActive = true, DisplayOrder = 3, CreatedAt = now, UpdatedAt = now },
            new Category { Id = 4, Name = "Sports & Outdoors", Slug = "sports-outdoors", Description = "Sports and outdoor equipment", IsActive = true, DisplayOrder = 4, CreatedAt = now, UpdatedAt = now },
            new Category { Id = 5, Name = "Books & Media", Slug = "books-media", Description = "Books, music, and media", IsActive = true, DisplayOrder = 5, CreatedAt = now, UpdatedAt = now },
            new Category { Id = 6, Name = "Health & Beauty", Slug = "health-beauty", Description = "Health and beauty products", IsActive = true, DisplayOrder = 6, CreatedAt = now, UpdatedAt = now },
            new Category { Id = 7, Name = "Toys & Games", Slug = "toys-games", Description = "Toys and games for all ages", IsActive = true, DisplayOrder = 7, CreatedAt = now, UpdatedAt = now },
            new Category { Id = 8, Name = "Food & Grocery", Slug = "food-grocery", Description = "Food and grocery items", IsActive = true, DisplayOrder = 8, CreatedAt = now, UpdatedAt = now }
        );
    }
}
