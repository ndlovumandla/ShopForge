using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Implementations;

public class InventoryService : IInventoryService
{
    private readonly ShopForgeDbContext _db;

    public InventoryService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<PagedResult<InventoryLogDto>>> GetLogsAsync(int page, int pageSize, int? productId = null)
    {
        var query = _db.InventoryLogs
            .Include(l => l.Product)
            .Include(l => l.ProductVariant)
            .Include(l => l.ChangedByUser)
            .AsQueryable();

        if (productId.HasValue)
            query = query.Where(l => l.ProductId == productId.Value);

        query = query.OrderByDescending(l => l.CreatedAt);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return ApiResponse<PagedResult<InventoryLogDto>>.Ok(new PagedResult<InventoryLogDto>
        {
            Items = items.Select(l => new InventoryLogDto
            {
                Id = l.Id,
                ProductId = l.ProductId,
                ProductName = l.Product?.Name ?? string.Empty,
                ProductVariantId = l.ProductVariantId,
                VariantName = l.ProductVariant?.Name,
                ChangeAmount = l.ChangeAmount,
                Reason = l.Reason,
                ReferenceId = l.ReferenceId,
                Note = l.Note,
                ChangedByUser = l.ChangedByUser != null
                    ? $"{l.ChangedByUser.FirstName} {l.ChangedByUser.LastName}"
                    : null,
                CreatedAt = l.CreatedAt
            }).ToList(),
            Page = page, PageSize = pageSize, TotalCount = total
        });
    }
}
