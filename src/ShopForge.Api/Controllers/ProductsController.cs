using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _products;

    public ProductsController(IProductService products) => _products = products;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ProductSummaryDto>>>> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] int? brandId = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] bool? inStock = null,
        [FromQuery] bool? isFeatured = null,
        [FromQuery] string? sortBy = null)
    {
        var result = await _products.GetProductsAsync(page, pageSize, search, categoryId, brandId, minPrice, maxPrice, inStock, isFeatured, sortBy);
        return Ok(result);
    }

    [HttpGet("featured")]
    public async Task<ActionResult<ApiResponse<List<ProductSummaryDto>>>> GetFeatured([FromQuery] int count = 8)
        => Ok(await _products.GetFeaturedProductsAsync(count));

    [HttpGet("new-arrivals")]
    public async Task<ActionResult<ApiResponse<List<ProductSummaryDto>>>> GetNewArrivals([FromQuery] int count = 8)
        => Ok(await _products.GetNewArrivalsAsync(count));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetById(int id)
    {
        var result = await _products.GetProductByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("slug/{slug}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetBySlug(string slug)
    {
        var result = await _products.GetProductBySlugAsync(slug);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("{id:int}/related")]
    public async Task<ActionResult<ApiResponse<List<ProductSummaryDto>>>> GetRelated(int id, [FromQuery] int count = 4)
        => Ok(await _products.GetRelatedProductsAsync(id, count));
}
