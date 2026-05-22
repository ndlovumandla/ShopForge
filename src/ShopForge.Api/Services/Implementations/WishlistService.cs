using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Implementations;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Api.Services.Implementations;

public class WishlistService : IWishlistService
{
    private readonly ShopForgeDbContext _db;

    public WishlistService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<List<ProductSummaryDto>>> GetWishlistAsync(int userId)
    {
        var items = await _db.Wishlists
            .Include(w => w.Product).ThenInclude(p => p.Category)
            .Include(w => w.Product).ThenInclude(p => p.Brand)
            .Include(w => w.Product).ThenInclude(p => p.Images)
            .Include(w => w.Product).ThenInclude(p => p.Reviews)
            .Where(w => w.UserId == userId)
            .Select(w => w.Product)
            .ToListAsync();

        return ApiResponse<List<ProductSummaryDto>>.Ok(items.Select(ProductService.MapToSummary).ToList());
    }

    public async Task<ApiResponse<bool>> AddToWishlistAsync(int userId, int productId)
    {
        if (await _db.Wishlists.AnyAsync(w => w.UserId == userId && w.ProductId == productId))
            return ApiResponse<bool>.Ok(true, "Already in wishlist.");

        _db.Wishlists.Add(new Wishlist
        {
            UserId = userId,
            ProductId = productId,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Added to wishlist.");
    }

    public async Task<ApiResponse<bool>> RemoveFromWishlistAsync(int userId, int productId)
    {
        var item = await _db.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
        if (item != null)
        {
            _db.Wishlists.Remove(item);
            await _db.SaveChangesAsync();
        }
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<bool>> IsInWishlistAsync(int userId, int productId)
    {
        var result = await _db.Wishlists.AnyAsync(w => w.UserId == userId && w.ProductId == productId);
        return ApiResponse<bool>.Ok(result);
    }
}
