using Microsoft.EntityFrameworkCore;
using ShopForge.Database.Entities;
using ShopForge.Database.SeedData;

namespace ShopForge.Database;

public class ShopForgeDbContext : DbContext
{
    public ShopForgeDbContext(DbContextOptions<ShopForgeDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Wishlist> Wishlists => Set<Wishlist>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<OrderStatusHistory> OrderStatusHistories => Set<OrderStatusHistory>();
    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<ShippingMethod> ShippingMethods => Set<ShippingMethod>();
    public DbSet<InventoryLog> InventoryLogs => Set<InventoryLog>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<AppSetting> AppSettings => Set<AppSetting>();
    public DbSet<BannerSlide> BannerSlides => Set<BannerSlide>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopForgeDbContext).Assembly);

        // Seed data
        CategorySeedData.Seed(modelBuilder);
        BrandSeedData.Seed(modelBuilder);
        ProductSeedData.Seed(modelBuilder);
        UserSeedData.Seed(modelBuilder);
        OrderSeedData.Seed(modelBuilder);
        CouponSeedData.Seed(modelBuilder);
        ShippingSeedData.Seed(modelBuilder);
        BannerSeedData.Seed(modelBuilder);
        AppSettingsSeedData.Seed(modelBuilder);
    }
}
