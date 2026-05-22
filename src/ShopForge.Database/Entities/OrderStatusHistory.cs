namespace ShopForge.Database.Entities;

public class OrderStatusHistory
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Note { get; set; }
    public int? ChangedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Order Order { get; set; } = null!;
    public User? ChangedByUser { get; set; }
}
