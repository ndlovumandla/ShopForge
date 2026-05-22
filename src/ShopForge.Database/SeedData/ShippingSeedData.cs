using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;

namespace ShopForge.Database.SeedData;

public static class ShippingSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<ShippingMethod>().HasData(
            new ShippingMethod { Id = 1, Name = "Standard Delivery", Description = "Delivered in 3-5 business days", Cost = 79m, EstimatedDaysMin = 3, EstimatedDaysMax = 5, IsActive = true, FreeShippingThreshold = 999m, CreatedAt = now },
            new ShippingMethod { Id = 2, Name = "Express Delivery", Description = "Delivered in 1-2 business days", Cost = 149m, EstimatedDaysMin = 1, EstimatedDaysMax = 2, IsActive = true, CreatedAt = now },
            new ShippingMethod { Id = 3, Name = "Overnight Courier", Description = "Delivered next business day", Cost = 299m, EstimatedDaysMin = 1, EstimatedDaysMax = 1, IsActive = true, CreatedAt = now }
        );
    }
}
