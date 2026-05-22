using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Controllers;

[Authorize(Policy = "AdminOrManager")]
[ApiController]
[Route("api/admin/coupons")]
public class AdminCouponsController : ControllerBase
{
    private readonly ICouponService _coupons;

    public AdminCouponsController(ICouponService coupons) => _coupons = coupons;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<CouponDto>>>> GetAll()
        => Ok(await _coupons.GetAllCouponsAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CouponDto>>> GetById(int id)
    {
        var result = await _coupons.GetCouponByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CouponDto>>> Create([FromBody] CreateCouponRequest request)
    {
        var result = await _coupons.CreateCouponAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<CouponDto>>> Update(int id, [FromBody] CreateCouponRequest request)
    {
        var result = await _coupons.UpdateCouponAsync(id, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _coupons.DeleteCouponAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}/toggle")]
    public async Task<ActionResult<ApiResponse<bool>>> Toggle(int id)
        => Ok(await _coupons.ToggleActiveAsync(id));
}
