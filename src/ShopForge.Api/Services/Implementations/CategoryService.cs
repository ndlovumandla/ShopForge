using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ShopForgeDbContext _db;

    public CategoryService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<List<CategoryDto>>> GetAllAsync(bool activeOnly = true)
    {
        var query = _db.Categories
            .Include(c => c.Products)
            .AsQueryable();

        if (activeOnly) query = query.Where(c => c.IsActive);

        var categories = await query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name).ToListAsync();
        return ApiResponse<List<CategoryDto>>.Ok(categories.Select(MapToDto).ToList());
    }

    public async Task<ApiResponse<List<CategoryDto>>> GetTreeAsync()
    {
        var all = await _db.Categories
            .Include(c => c.SubCategories).ThenInclude(sc => sc.Products)
            .Include(c => c.Products)
            .Where(c => c.IsActive && c.ParentCategoryId == null)
            .OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name)
            .ToListAsync();

        return ApiResponse<List<CategoryDto>>.Ok(all.Select(MapToDtoWithChildren).ToList());
    }

    public async Task<ApiResponse<CategoryDto>> GetByIdAsync(int id)
    {
        var cat = await _db.Categories
            .Include(c => c.SubCategories)
            .Include(c => c.Products)
            .Include(c => c.ParentCategory)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cat == null) return ApiResponse<CategoryDto>.Fail("Category not found.");
        return ApiResponse<CategoryDto>.Ok(MapToDtoWithChildren(cat));
    }

    public async Task<ApiResponse<CategoryDto>> GetBySlugAsync(string slug)
    {
        var cat = await _db.Categories
            .Include(c => c.SubCategories)
            .Include(c => c.Products)
            .Include(c => c.ParentCategory)
            .FirstOrDefaultAsync(c => c.Slug == slug);

        if (cat == null) return ApiResponse<CategoryDto>.Fail("Category not found.");
        return ApiResponse<CategoryDto>.Ok(MapToDtoWithChildren(cat));
    }

    public async Task<ApiResponse<CategoryDto>> CreateAsync(CreateCategoryRequest request)
    {
        if (await _db.Categories.AnyAsync(c => c.Slug == request.Slug))
            return ApiResponse<CategoryDto>.Fail("A category with this slug already exists.");

        var cat = new Category
        {
            Name = request.Name,
            Slug = request.Slug,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            ParentCategoryId = request.ParentCategoryId,
            IsActive = request.IsActive,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Categories.Add(cat);
        await _db.SaveChangesAsync();
        return await GetByIdAsync(cat.Id);
    }

    public async Task<ApiResponse<CategoryDto>> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var cat = await _db.Categories.FindAsync(id);
        if (cat == null) return ApiResponse<CategoryDto>.Fail("Category not found.");

        if (await _db.Categories.AnyAsync(c => c.Slug == request.Slug && c.Id != id))
            return ApiResponse<CategoryDto>.Fail("A category with this slug already exists.");

        cat.Name = request.Name;
        cat.Slug = request.Slug;
        cat.Description = request.Description;
        cat.ImageUrl = request.ImageUrl;
        cat.ParentCategoryId = request.ParentCategoryId;
        cat.IsActive = request.IsActive;
        cat.DisplayOrder = request.DisplayOrder;
        cat.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var cat = await _db.Categories.FindAsync(id);
        if (cat == null) return ApiResponse<bool>.Fail("Category not found.");
        cat.IsActive = false;
        cat.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    private static CategoryDto MapToDto(Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Slug = c.Slug,
        Description = c.Description,
        ImageUrl = c.ImageUrl,
        ParentCategoryId = c.ParentCategoryId,
        IsActive = c.IsActive,
        DisplayOrder = c.DisplayOrder,
        ProductCount = c.Products?.Count ?? 0
    };

    private static CategoryDto MapToDtoWithChildren(Category c)
    {
        var dto = MapToDto(c);
        dto.ParentCategoryName = c.ParentCategory?.Name;
        dto.SubCategories = c.SubCategories?.Select(MapToDtoWithChildren).ToList() ?? new();
        return dto;
    }
}
