using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Api.Services.Interfaces;

public interface IProductService
{
    Task<ApiResponse<PagedResult<ProductSummaryDto>>> GetProductsAsync(int page, int pageSize, string? search, int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, bool? inStock, bool? isFeatured, string? sortBy);
    Task<ApiResponse<ProductDto>> GetProductByIdAsync(int id);
    Task<ApiResponse<ProductDto>> GetProductBySlugAsync(string slug);
    Task<ApiResponse<List<ProductSummaryDto>>> GetFeaturedProductsAsync(int count = 8);
    Task<ApiResponse<List<ProductSummaryDto>>> GetNewArrivalsAsync(int count = 8);
    Task<ApiResponse<List<ProductSummaryDto>>> GetRelatedProductsAsync(int productId, int count = 4);
    Task<ApiResponse<ProductDto>> CreateProductAsync(CreateProductRequest request, int createdByUserId);
    Task<ApiResponse<ProductDto>> UpdateProductAsync(int id, UpdateProductRequest request);
    Task<ApiResponse<bool>> DeleteProductAsync(int id);
    Task<ApiResponse<bool>> AdjustStockAsync(int productId, AdjustStockRequest request, int userId);
    Task<ApiResponse<ProductDto>> AddImageAsync(int productId, AddProductImageRequest request);
    Task<ApiResponse<bool>> DeleteImageAsync(int productId, int imageId);
    Task<ApiResponse<bool>> SetPrimaryImageAsync(int productId, int imageId);
}
