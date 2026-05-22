using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly ShopForgeDbContext _db;

    public AddressesController(ShopForgeDbContext db) => _db = db;

    private int GetUserId() => int.Parse(User.FindFirstValue(AppConstants.JwtClaims.UserId)!);

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<AddressDto>>>> GetAll()
    {
        var userId = GetUserId();
        var addresses = await _db.Addresses.Where(a => a.UserId == userId).OrderByDescending(a => a.IsDefault).ToListAsync();
        return Ok(ApiResponse<List<AddressDto>>.Ok(addresses.Select(MapToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<AddressDto>>> GetById(int id)
    {
        var userId = GetUserId();
        var address = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (address == null) return NotFound(ApiResponse<AddressDto>.Fail("Address not found."));
        return Ok(ApiResponse<AddressDto>.Ok(MapToDto(address)));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AddressDto>>> Create([FromBody] CreateAddressRequest request)
    {
        var userId = GetUserId();

        if (request.IsDefault)
        {
            var existing = await _db.Addresses.Where(a => a.UserId == userId && a.IsDefault).ToListAsync();
            existing.ForEach(a => a.IsDefault = false);
        }

        var address = new Address
        {
            UserId = userId,
            Label = request.Label,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Line1 = request.Line1,
            Line2 = request.Line2,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            IsDefault = request.IsDefault,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Addresses.Add(address);
        await _db.SaveChangesAsync();

        return Ok(ApiResponse<AddressDto>.Ok(MapToDto(address)));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<AddressDto>>> Update(int id, [FromBody] CreateAddressRequest request)
    {
        var userId = GetUserId();
        var address = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (address == null) return NotFound(ApiResponse<AddressDto>.Fail("Address not found."));

        if (request.IsDefault && !address.IsDefault)
        {
            var existing = await _db.Addresses.Where(a => a.UserId == userId && a.IsDefault).ToListAsync();
            existing.ForEach(a => a.IsDefault = false);
        }

        address.Label = request.Label;
        address.FullName = request.FullName;
        address.PhoneNumber = request.PhoneNumber;
        address.Line1 = request.Line1;
        address.Line2 = request.Line2;
        address.City = request.City;
        address.State = request.State;
        address.PostalCode = request.PostalCode;
        address.Country = request.Country;
        address.IsDefault = request.IsDefault;
        address.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return Ok(ApiResponse<AddressDto>.Ok(MapToDto(address)));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var userId = GetUserId();
        var address = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (address == null) return NotFound(ApiResponse<bool>.Fail("Address not found."));
        _db.Addresses.Remove(address);
        await _db.SaveChangesAsync();
        return Ok(ApiResponse<bool>.Ok(true));
    }

    [HttpPut("{id:int}/set-default")]
    public async Task<ActionResult<ApiResponse<bool>>> SetDefault(int id)
    {
        var userId = GetUserId();
        var addresses = await _db.Addresses.Where(a => a.UserId == userId).ToListAsync();
        foreach (var addr in addresses)
        {
            addr.IsDefault = addr.Id == id;
            addr.UpdatedAt = DateTime.UtcNow;
        }
        await _db.SaveChangesAsync();
        return Ok(ApiResponse<bool>.Ok(true));
    }

    private static AddressDto MapToDto(Address a) => new()
    {
        Id = a.Id,
        Label = a.Label,
        FullName = a.FullName,
        PhoneNumber = a.PhoneNumber,
        Line1 = a.Line1,
        Line2 = a.Line2,
        City = a.City,
        State = a.State,
        PostalCode = a.PostalCode,
        Country = a.Country,
        IsDefault = a.IsDefault
    };
}
