using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Controllers;

[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/admin/users")]
public class AdminUsersController : ControllerBase
{
    private readonly ShopForgeDbContext _db;

    public AdminUsersController(ShopForgeDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<AdminUserDto>>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] string? role = null)
    {
        var query = _db.Users.Include(u => u.Orders).AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(u => u.Email.Contains(search) ||
                u.FirstName.Contains(search) || u.LastName.Contains(search));

        if (!string.IsNullOrEmpty(role))
            query = query.Where(u => u.Role == role);

        query = query.OrderByDescending(u => u.CreatedAt);

        var total = await query.CountAsync();
        var users = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return Ok(ApiResponse<PagedResult<AdminUserDto>>.Ok(new PagedResult<AdminUserDto>
        {
            Items = users.Select(MapAdminUser).ToList(),
            Page = page, PageSize = pageSize, TotalCount = total
        }));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<AdminUserDto>>> GetById(int id)
    {
        var user = await _db.Users.Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound(ApiResponse<AdminUserDto>.Fail("User not found."));
        return Ok(ApiResponse<AdminUserDto>.Ok(MapAdminUser(user)));
    }

    [HttpPut("{id:int}/role")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateRole(int id, [FromBody] UpdateRoleRequest request)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound(ApiResponse<bool>.Fail("User not found."));
        user.Role = request.Role;
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(ApiResponse<bool>.Ok(true));
    }

    [HttpPut("{id:int}/toggle-active")]
    public async Task<ActionResult<ApiResponse<bool>>> ToggleActive(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound(ApiResponse<bool>.Fail("User not found."));
        user.IsActive = !user.IsActive;
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(ApiResponse<bool>.Ok(user.IsActive));
    }

    private static AdminUserDto MapAdminUser(User user)
    {
        return new AdminUserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            IsActive = user.IsActive,
            EmailVerified = user.EmailVerified,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt,
            OrderCount = user.Orders.Count,
            TotalSpent = user.Orders.Sum(o => o.TotalAmount)
        };
    }
}

public class UpdateRoleRequest
{
    public string Role { get; set; } = string.Empty;
}
