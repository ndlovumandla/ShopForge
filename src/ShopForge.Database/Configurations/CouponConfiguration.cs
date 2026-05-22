using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopForge.Database.Entities;

namespace ShopForge.Database.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.DiscountType).HasMaxLength(50).IsRequired();
        builder.Property(x => x.DiscountValue).HasColumnType("decimal(18,2)");
        builder.Property(x => x.MinimumOrderAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.MaximumDiscountAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.StartsAt).HasColumnType("datetime2");
        builder.Property(x => x.ExpiresAt).HasColumnType("datetime2");
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2");
        builder.Property(x => x.UpdatedAt).HasColumnType("datetime2");
    }
}
