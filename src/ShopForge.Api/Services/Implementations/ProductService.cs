using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Products;
using System.Text.RegularExpressions;

namespace ShopForge.Api.Services.Implementations;

public class ProductService : IProductService
{
    private readonly ShopForgeDbContext _db;

    public ProductService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<PagedResult<ProductSummaryDto>>> GetProductsAsync(
        int page, int pageSize, string? search, int? categoryId, int? brandId,
        decimal? minPrice, decimal? maxPrice, bool? inStock, bool? isFeatured, string? sortBy)
    {
        var query = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Images)
            .Include(p => p.Reviews)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search) || p.SKU.Contains(search) || (p.Tags != null && p.Tags.Contains(search)));

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        if (brandId.HasValue)
            query = query.Where(p => p.BrandId == brandId.Value);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (inStock.HasValue)
            query = inStock.Value ? query.Where(p => p.StockQuantity > 0) : query.Where(p => p.StockQuantity == 0);

        if (isFeatured.HasValue)
            query = query.Where(p => p.IsFeatured == isFeatured.Value);

        query = sortBy?.ToLower() switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "newest" => query.OrderByDescending(p => p.CreatedAt),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return ApiResponse<PagedResult<ProductSummaryDto>>.Ok(new PagedResult<ProductSummaryDto>
        {
            Items = items.Select(MapToSummary).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = total
        });
    }

    public async Task<ApiResponse<ProductDto>> GetProductByIdAsync(int id)
    {
        var product = await LoadFullProductQuery().FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return ApiResponse<ProductDto>.Fail("Product not found.");
        return ApiResponse<ProductDto>.Ok(MapToDto(product));
    }

    public async Task<ApiResponse<ProductDto>> GetProductBySlugAsync(string slug)
    {
        var product = await LoadFullProductQuery().FirstOrDefaultAsync(p => p.Slug == slug);
        if (product == null) return ApiResponse<ProductDto>.Fail("Product not found.");
        return ApiResponse<ProductDto>.Ok(MapToDto(product));
    }

    public async Task<ApiResponse<List<ProductSummaryDto>>> GetFeaturedProductsAsync(int count = 8)
    {
        var products = await _db.Products
            .Include(p => p.Category).Include(p => p.Brand).Include(p => p.Images).Include(p => p.Reviews)
            .Where(p => p.IsActive && p.IsFeatured)
            .OrderByDescending(p => p.CreatedAt)
            .Take(count).ToListAsync();
        return ApiResponse<List<ProductSummaryDto>>.Ok(products.Select(MapToSummary).ToList());
    }

    public async Task<ApiResponse<List<ProductSummaryDto>>> GetNewArrivalsAsync(int count = 8)
    {
        var products = await _db.Products
            .Include(p => p.Category).Include(p => p.Brand).Include(p => p.Images).Include(p => p.Reviews)
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .Take(count).ToListAsync();
        return ApiResponse<List<ProductSummaryDto>>.Ok(products.Select(MapToSummary).ToList());
    }

    public async Task<ApiResponse<List<ProductSummaryDto>>> GetRelatedProductsAsync(int productId, int count = 4)
    {
        var product = await _db.Products.FindAsync(productId);
        if (product == null) return ApiResponse<List<ProductSummaryDto>>.Ok(new());

        var related = await _db.Products
            .Include(p => p.Category).Include(p => p.Brand).Include(p => p.Images).Include(p => p.Reviews)
            .Where(p => p.IsActive && p.Id != productId && p.CategoryId == product.CategoryId)
            .OrderByDescending(p => p.CreatedAt)
            .Take(count).ToListAsync();

        return ApiResponse<List<ProductSummaryDto>>.Ok(related.Select(MapToSummary).ToList());
    }

    public async Task<ApiResponse<ProductDto>> CreateProductAsync(CreateProductRequest request, int createdByUserId)
    {
        request.Slug = NormalizeSlug(request.Slug, request.Name);

        if (!await _db.Categories.AnyAsync(c => c.Id == request.CategoryId))
            return ApiResponse<ProductDto>.Fail("Please select a valid category.");

        if (await _db.Products.AnyAsync(p => p.SKU == request.SKU))
            return ApiResponse<ProductDto>.Fail("A product with this SKU already exists.");

        if (await _db.Products.AnyAsync(p => p.Slug == request.Slug))
            return ApiResponse<ProductDto>.Fail("A product with this slug already exists.");

        var product = new Product
        {
            Name = request.Name,
            Slug = request.Slug,
            Description = request.Description,
            ShortDescription = request.ShortDescription,
            SKU = request.SKU,
            Barcode = request.Barcode,
            Price = request.Price,
            CompareAtPrice = request.CompareAtPrice,
            CostPrice = request.CostPrice,
            StockQuantity = request.StockQuantity,
            LowStockThreshold = request.LowStockThreshold,
            TrackInventory = request.TrackInventory,
            Weight = request.Weight,
            Width = request.Width,
            Height = request.Height,
            Depth = request.Depth,
            IsActive = request.IsActive,
            IsFeatured = request.IsFeatured,
            IsDigital = request.IsDigital,
            Tags = request.Tags,
            MetaTitle = request.MetaTitle,
            MetaDescription = request.MetaDescription,
            CategoryId = request.CategoryId,
            BrandId = request.BrandId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        await UpsertPrimaryImageAsync(product.Id, request.PrimaryImageUrl, request.PrimaryImageAltText ?? request.Name);

        // Log initial stock
        if (product.StockQuantity > 0)
        {
            _db.InventoryLogs.Add(new InventoryLog
            {
                ProductId = product.Id,
                ChangeAmount = product.StockQuantity,
                Reason = "Initial stock",
                ChangedByUserId = createdByUserId,
                CreatedAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
        }

        return await GetProductByIdAsync(product.Id);
    }

    public async Task<ApiResponse<ProductDto>> UpdateProductAsync(int id, UpdateProductRequest request)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return ApiResponse<ProductDto>.Fail("Product not found.");

        request.Slug = NormalizeSlug(request.Slug, request.Name);

        if (!await _db.Categories.AnyAsync(c => c.Id == request.CategoryId))
            return ApiResponse<ProductDto>.Fail("Please select a valid category.");

        if (await _db.Products.AnyAsync(p => p.SKU == request.SKU && p.Id != id))
            return ApiResponse<ProductDto>.Fail("A product with this SKU already exists.");

        if (await _db.Products.AnyAsync(p => p.Slug == request.Slug && p.Id != id))
            return ApiResponse<ProductDto>.Fail("A product with this slug already exists.");

        product.Name = request.Name;
        product.Slug = request.Slug;
        product.Description = request.Description;
        product.ShortDescription = request.ShortDescription;
        product.SKU = request.SKU;
        product.Barcode = request.Barcode;
        product.Price = request.Price;
        product.CompareAtPrice = request.CompareAtPrice;
        product.CostPrice = request.CostPrice;
        product.StockQuantity = request.StockQuantity;
        product.LowStockThreshold = request.LowStockThreshold;
        product.TrackInventory = request.TrackInventory;
        product.Weight = request.Weight;
        product.Width = request.Width;
        product.Height = request.Height;
        product.Depth = request.Depth;
        product.IsActive = request.IsActive;
        product.IsFeatured = request.IsFeatured;
        product.IsDigital = request.IsDigital;
        product.Tags = request.Tags;
        product.MetaTitle = request.MetaTitle;
        product.MetaDescription = request.MetaDescription;
        product.CategoryId = request.CategoryId;
        product.BrandId = request.BrandId;
        product.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        await UpsertPrimaryImageAsync(product.Id, request.PrimaryImageUrl, request.PrimaryImageAltText ?? request.Name);
        return await GetProductByIdAsync(id);
    }

    public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return ApiResponse<bool>.Fail("Product not found.");
        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Product deleted.");
    }

    public async Task<ApiResponse<bool>> AdjustStockAsync(int productId, AdjustStockRequest request, int userId)
    {
        var product = await _db.Products.FindAsync(productId);
        if (product == null) return ApiResponse<bool>.Fail("Product not found.");

        product.StockQuantity += request.ChangeAmount;
        if (product.StockQuantity < 0) product.StockQuantity = 0;
        product.UpdatedAt = DateTime.UtcNow;

        _db.InventoryLogs.Add(new InventoryLog
        {
            ProductId = productId,
            ChangeAmount = request.ChangeAmount,
            Reason = request.Reason,
            Note = request.Note,
            ChangedByUserId = userId,
            CreatedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Stock adjusted.");
    }

    public async Task<ApiResponse<ProductDto>> AddImageAsync(int productId, AddProductImageRequest request)
    {
        var product = await _db.Products.FindAsync(productId);
        if (product == null) return ApiResponse<ProductDto>.Fail("Product not found.");

        if (request.IsPrimary)
        {
            var existingPrimary = await _db.ProductImages.Where(i => i.ProductId == productId && i.IsPrimary).ToListAsync();
            existingPrimary.ForEach(i => i.IsPrimary = false);
        }

        _db.ProductImages.Add(new ProductImage
        {
            ProductId = productId,
            ImageUrl = request.ImageUrl,
            AltText = request.AltText,
            IsPrimary = request.IsPrimary,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return await GetProductByIdAsync(productId);
    }

    public async Task<ApiResponse<bool>> DeleteImageAsync(int productId, int imageId)
    {
        var image = await _db.ProductImages.FirstOrDefaultAsync(i => i.Id == imageId && i.ProductId == productId);
        if (image == null) return ApiResponse<bool>.Fail("Image not found.");
        _db.ProductImages.Remove(image);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<bool>> SetPrimaryImageAsync(int productId, int imageId)
    {
        var images = await _db.ProductImages.Where(i => i.ProductId == productId).ToListAsync();
        foreach (var img in images)
            img.IsPrimary = img.Id == imageId;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    private IQueryable<Product> LoadFullProductQuery() =>
        _db.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Images.OrderBy(i => i.DisplayOrder))
            .Include(p => p.Variants)
            .Include(p => p.Attributes.OrderBy(a => a.DisplayOrder))
            .Include(p => p.Reviews);

    private static string NormalizeSlug(string? slug, string name)
    {
        var source = string.IsNullOrWhiteSpace(slug) ? name : slug;
        var normalized = source.Trim().ToLowerInvariant();
        normalized = Regex.Replace(normalized, @"[^a-z0-9]+", "-").Trim('-');
        return string.IsNullOrWhiteSpace(normalized) ? Guid.NewGuid().ToString("N") : normalized;
    }

    private async Task UpsertPrimaryImageAsync(int productId, string? imageUrl, string? altText)
    {
        imageUrl = imageUrl?.Trim();
        altText = altText?.Trim();

        if (string.IsNullOrWhiteSpace(imageUrl))
            return;

        var images = await _db.ProductImages.Where(i => i.ProductId == productId).ToListAsync();
        foreach (var image in images)
            image.IsPrimary = false;

        var primary = images.OrderBy(i => i.DisplayOrder).FirstOrDefault();
        if (primary == null)
        {
            _db.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                ImageUrl = imageUrl,
                AltText = altText,
                IsPrimary = true,
                DisplayOrder = 0,
                CreatedAt = DateTime.UtcNow
            });
        }
        else
        {
            primary.ImageUrl = imageUrl;
            primary.AltText = altText;
            primary.IsPrimary = true;
            primary.DisplayOrder = 0;
        }

        await _db.SaveChangesAsync();
    }

    internal static ProductSummaryDto MapToSummary(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Slug = p.Slug,
        ShortDescription = p.ShortDescription,
        SKU = p.SKU,
        Price = p.Price,
        CompareAtPrice = p.CompareAtPrice,
        StockQuantity = p.StockQuantity,
        IsActive = p.IsActive,
        IsFeatured = p.IsFeatured,
        PrimaryImageUrl = p.Images.FirstOrDefault(i => i.IsPrimary)?.ImageUrl ?? p.Images.FirstOrDefault()?.ImageUrl,
        CategoryName = p.Category?.Name,
        CategoryId = p.CategoryId,
        BrandName = p.Brand?.Name,
        BrandId = p.BrandId,
        AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => (double)r.Rating) : 0,
        ReviewCount = p.Reviews.Count,
        LowStockThreshold = p.LowStockThreshold,
        CreatedAt = p.CreatedAt
    };

    internal static ProductDto MapToDto(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Slug = p.Slug,
        Description = p.Description,
        ShortDescription = p.ShortDescription,
        SKU = p.SKU,
        Barcode = p.Barcode,
        Price = p.Price,
        CompareAtPrice = p.CompareAtPrice,
        CostPrice = p.CostPrice,
        StockQuantity = p.StockQuantity,
        LowStockThreshold = p.LowStockThreshold,
        TrackInventory = p.TrackInventory,
        Weight = p.Weight,
        Width = p.Width,
        Height = p.Height,
        Depth = p.Depth,
        IsActive = p.IsActive,
        IsFeatured = p.IsFeatured,
        IsDigital = p.IsDigital,
        Tags = p.Tags,
        MetaTitle = p.MetaTitle,
        MetaDescription = p.MetaDescription,
        CategoryId = p.CategoryId,
        CategoryName = p.Category?.Name,
        BrandId = p.BrandId,
        BrandName = p.Brand?.Name,
        AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => (double)r.Rating) : 0,
        ReviewCount = p.Reviews.Count,
        PrimaryImageUrl = p.Images.FirstOrDefault(i => i.IsPrimary)?.ImageUrl ?? p.Images.FirstOrDefault()?.ImageUrl,
        CreatedAt = p.CreatedAt,
        Images = p.Images.Select(i => new ProductImageDto
        {
            Id = i.Id,
            ImageUrl = i.ImageUrl,
            AltText = i.AltText,
            IsPrimary = i.IsPrimary,
            DisplayOrder = i.DisplayOrder
        }).ToList(),
        Variants = p.Variants.Select(v => new ProductVariantDto
        {
            Id = v.Id,
            Name = v.Name,
            SKU = v.SKU,
            Price = v.Price,
            StockQuantity = v.StockQuantity,
            IsActive = v.IsActive
        }).ToList(),
        Attributes = p.Attributes.Select(a => new ProductAttributeDto
        {
            Id = a.Id,
            AttributeName = a.AttributeName,
            AttributeValue = a.AttributeValue,
            DisplayOrder = a.DisplayOrder
        }).ToList()
    };
}
