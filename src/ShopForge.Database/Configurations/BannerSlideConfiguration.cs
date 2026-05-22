using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopForge.Database.Entities;

namespace ShopForge.Database.Configurations;

public class BannerSlideConfiguration : IEntityTypeConfiguration<BannerSlide>
{
    public void Configure(EntityTypeBuilder<BannerSlide> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(200);
        builder.Property(x => x.SubTitle).HasMaxLength(300);
        builder.Property(x => x.ImageUrl).HasMaxLength(500).IsRequired();
        builder.Property(x => x.LinkUrl).HasMaxLength(500);
        builder.Property(x => x.ButtonText).HasMaxLength(50);
        builder.Property(x => x.StartsAt).HasColumnType("datetime2");
        builder.Property(x => x.ExpiresAt).HasColumnType("datetime2");
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2");
    }
}
