namespace ShopForge.Database.Entities;

public class ShippingMethod
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public int EstimatedDaysMin { get; set; }
    public int EstimatedDaysMax { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal? FreeShippingThreshold { get; set; }
    public DateTime CreatedAt { get; set; }
}
