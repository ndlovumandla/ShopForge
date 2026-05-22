namespace ShopForge.Database.Entities;

public class InventoryLog
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }
    public int ChangeAmount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public int? ReferenceId { get; set; }
    public string? Note { get; set; }
    public int? ChangedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Product Product { get; set; } = null!;
    public ProductVariant? ProductVariant { get; set; }
    public User? ChangedByUser { get; set; }
}
