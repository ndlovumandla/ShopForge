using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Interfaces;

public class InventoryLogDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int? ProductVariantId { get; set; }
    public string? VariantName { get; set; }
    public int ChangeAmount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public int? ReferenceId { get; set; }
    public string? Note { get; set; }
    public string? ChangedByUser { get; set; }
    public DateTime CreatedAt { get; set; }
}

public interface IInventoryService
{
    Task<ApiResponse<PagedResult<InventoryLogDto>>> GetLogsAsync(int page, int pageSize, int? productId = null);
}
