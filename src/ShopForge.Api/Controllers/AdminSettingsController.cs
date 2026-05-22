using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Controllers;

[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/admin/settings")]
public class AdminSettingsController : ControllerBase
{
    private readonly ISettingsService _settings;

    public AdminSettingsController(ISettingsService settings) => _settings = settings;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<AppSettingDto>>>> GetAll()
        => Ok(await _settings.GetAllSettingsAsync());

    [HttpPut]
    public async Task<ActionResult<ApiResponse<bool>>> Update([FromBody] UpdateSettingsRequest request)
    {
        var result = await _settings.UpdateSettingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // Shipping methods
    [HttpGet("shipping")]
    [Authorize(Policy = "AdminOrManager")]
    public async Task<ActionResult<ApiResponse<List<ShippingMethodDto>>>> GetShipping()
        => Ok(await _settings.GetShippingMethodsAsync());

    [HttpPost("shipping")]
    public async Task<ActionResult<ApiResponse<ShippingMethodDto>>> UpsertShipping([FromBody] ShippingMethodDto dto)
    {
        var result = await _settings.UpsertShippingMethodAsync(dto);
        return Ok(result);
    }

    [HttpDelete("shipping/{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteShipping(int id)
    {
        var result = await _settings.DeleteShippingMethodAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    // Banners
    [HttpGet("banners")]
    [Authorize(Policy = "AdminOrManager")]
    public async Task<ActionResult<ApiResponse<List<BannerSlideDto>>>> GetBanners()
        => Ok(await _settings.GetBannersAsync());

    [HttpPost("banners")]
    public async Task<ActionResult<ApiResponse<BannerSlideDto>>> UpsertBanner([FromBody] BannerSlideDto dto)
        => Ok(await _settings.UpsertBannerAsync(dto));

    [HttpDelete("banners/{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteBanner(int id)
    {
        var result = await _settings.DeleteBannerAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    // Brands
    [HttpGet("brands")]
    [Authorize(Policy = "AdminOrManager")]
    public async Task<ActionResult<ApiResponse<List<BrandDto>>>> GetBrands()
        => Ok(await _settings.GetBrandsAsync());

    [HttpPost("brands")]
    public async Task<ActionResult<ApiResponse<BrandDto>>> CreateBrand([FromBody] CreateBrandRequest dto)
        => Ok(await _settings.UpsertBrandAsync(dto));

    [HttpPut("brands/{id:int}")]
    public async Task<ActionResult<ApiResponse<BrandDto>>> UpdateBrand(int id, [FromBody] CreateBrandRequest dto)
    {
        var result = await _settings.UpdateBrandAsync(id, dto);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("brands/{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteBrand(int id)
    {
        var result = await _settings.DeleteBrandAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    // Inventory logs
    [HttpGet("inventory-logs")]
    [Authorize(Policy = "AdminOrManager")]
    public async Task<ActionResult<ApiResponse<PagedResult<InventoryLogDto>>>> GetInventoryLogs(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] int? productId = null,
        [FromServices] IInventoryService inventoryService = null!)
        => Ok(await inventoryService.GetLogsAsync(page, pageSize, productId));
}
