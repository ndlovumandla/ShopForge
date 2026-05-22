using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopForge.Database.Entities;

namespace ShopForge.Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(200).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.Property(x => x.SKU).HasMaxLength(50).IsRequired();
        builder.HasIndex(x => x.SKU).IsUnique();
        builder.Property(x => x.Barcode).HasMaxLength(50);
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CompareAtPrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CostPrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Weight).HasColumnType("decimal(10,3)");
        builder.Property(x => x.Width).HasColumnType("decimal(10,2)");
        builder.Property(x => x.Height).HasColumnType("decimal(10,2)");
        builder.Property(x => x.Depth).HasColumnType("decimal(10,2)");
        builder.Property(x => x.Tags).HasMaxLength(500);
        builder.Property(x => x.MetaTitle).HasMaxLength(200);
        builder.Property(x => x.MetaDescription).HasMaxLength(500);
        builder.Property(x => x.ShortDescription).HasMaxLength(500);
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2");
        builder.Property(x => x.UpdatedAt).HasColumnType("datetime2");
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Brand)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
