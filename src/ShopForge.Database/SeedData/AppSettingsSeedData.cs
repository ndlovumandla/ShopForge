using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;

namespace ShopForge.Database.SeedData;

public static class AppSettingsSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<AppSetting>().HasData(
            new AppSetting { Id = 1, Key = "StoreName", Value = "ShopForge", Description = "The name of the store", UpdatedAt = now },
            new AppSetting { Id = 2, Key = "StoreEmail", Value = "hello@shopforge.co.za", Description = "Store contact email", UpdatedAt = now },
            new AppSetting { Id = 3, Key = "Currency", Value = "ZAR", Description = "Store currency", UpdatedAt = now },
            new AppSetting { Id = 4, Key = "TaxRate", Value = "0.15", Description = "Tax rate (15% VAT)", UpdatedAt = now },
            new AppSetting { Id = 5, Key = "FreeShippingThreshold", Value = "999.00", Description = "Free shipping on orders above this amount", UpdatedAt = now },
            new AppSetting { Id = 6, Key = "MaintenanceMode", Value = "false", Description = "Enable maintenance mode", UpdatedAt = now },
            new AppSetting { Id = 7, Key = "DefaultLowStockThreshold", Value = "10", Description = "Default low stock alert threshold", UpdatedAt = now }
        );
    }
}
