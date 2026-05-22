using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Products;

namespace ShopForge.Api.Controllers;

[Authorize(Policy = "AdminOrManager")]
[ApiController]
[Route("api/admin/products")]
public class AdminProductsController : ControllerBase
{
    private readonly IProductService _products;
    private readonly ICategoryService _categories;

    public AdminProductsController(IProductService products, ICategoryService categories)
    {
        _products = products;
        _categories = categories;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(AppConstants.JwtClaims.UserId)!);

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ProductSummaryDto>>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] int? brandId = null,
        [FromQuery] bool? inStock = null,
        [FromQuery] string? sortBy = null)
        => Ok(await _products.GetProductsAsync(page, pageSize, search, categoryId, brandId, null, null, inStock, null, sortBy));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetById(int id)
    {
        var result = await _products.GetProductByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductDto>>> Create([FromBody] CreateProductRequest request)
    {
        var result = await _products.CreateProductAsync(request, GetUserId());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var result = await _products.UpdateProductAsync(id, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _products.DeleteProductAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost("{id:int}/stock")]
    public async Task<ActionResult<ApiResponse<bool>>> AdjustStock(int id, [FromBody] AdjustStockRequest request)
    {
        var result = await _products.AdjustStockAsync(id, request, GetUserId());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{id:int}/images")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> AddImage(int id, [FromBody] AddProductImageRequest request)
    {
        var result = await _products.AddImageAsync(id, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id:int}/images/{imageId:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteImage(int id, int imageId)
    {
        var result = await _products.DeleteImageAsync(id, imageId);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}/images/{imageId:int}/primary")]
    public async Task<ActionResult<ApiResponse<bool>>> SetPrimaryImage(int id, int imageId)
        => Ok(await _products.SetPrimaryImageAsync(id, imageId));

    // Category management
    [HttpGet("categories")]
    public async Task<ActionResult<ApiResponse<List<CategoryDto>>>> GetCategories()
        => Ok(await _categories.GetAllAsync(false));

    [HttpPost("categories")]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var result = await _categories.CreateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("categories/{id:int}")]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> UpdateCategory(int id, [FromBody] UpdateCategoryRequest request)
    {
        var result = await _categories.UpdateAsync(id, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("categories/{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteCategory(int id)
    {
        var result = await _categories.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
