using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;

namespace ShopForge.Database.SeedData;

public static class UserSeedData
{
    // BCrypt cost-4 hashes (cost 4 for fast migration generation).
    private static readonly string _adminHash = BCrypt.Net.BCrypt.HashPassword("Admin@123", 4);
    private static readonly string _managerHash = BCrypt.Net.BCrypt.HashPassword("Manager@123", 4);
    private static readonly string _customerHash = BCrypt.Net.BCrypt.HashPassword("Customer@123", 4);

    public static void Seed(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var users = new List<User>
        {
            new User { Id = 1, Email = "admin@shopforge.co.za", PasswordHash = _adminHash, FirstName = "Admin", LastName = "User", Role = "Admin", IsActive = true, EmailVerified = true, CreatedAt = now, UpdatedAt = now },
            new User { Id = 2, Email = "manager@shopforge.co.za", PasswordHash = _managerHash, FirstName = "Manager", LastName = "User", Role = "Manager", IsActive = true, EmailVerified = true, CreatedAt = now, UpdatedAt = now }
        };

        for (var i = 0; i < 12; i++)
        {
            var id = i + 3;
            users.Add(new User
            {
                Id = id,
                Email = $"customer{id:D3}@example.com",
                PasswordHash = _customerHash,
                FirstName = $"Customer{id:D3}",
                LastName = "Demo",
                Role = "Customer",
                IsActive = true,
                EmailVerified = true,
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        modelBuilder.Entity<User>().HasData(users);
    }
}
