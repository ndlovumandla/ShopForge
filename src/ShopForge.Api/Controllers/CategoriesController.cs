using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categories;

    public CategoriesController(ICategoryService categories) => _categories = categories;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<CategoryDto>>>> GetAll([FromQuery] bool activeOnly = true)
        => Ok(await _categories.GetAllAsync(activeOnly));

    [HttpGet("tree")]
    public async Task<ActionResult<ApiResponse<List<CategoryDto>>>> GetTree()
        => Ok(await _categories.GetTreeAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> GetById(int id)
    {
        var result = await _categories.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("slug/{slug}")]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> GetBySlug(string slug)
    {
        var result = await _categories.GetBySlugAsync(slug);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
