namespace ShopForge.Database.Entities;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public string? TransactionId { get; set; }
    public string? CardLastFour { get; set; }
    public string? CardBrand { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? FailureReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Order Order { get; set; } = null!;
}
