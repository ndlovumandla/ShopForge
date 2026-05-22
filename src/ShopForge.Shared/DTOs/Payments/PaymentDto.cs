namespace ShopForge.Shared.DTOs.Payments;

public class ProcessPaymentRequest
{
    public int OrderId { get; set; }
    public string Method { get; set; } = "MockCard";
    public string? CardNumber { get; set; }
    public int? ExpiryMonth { get; set; }
    public int? ExpiryYear { get; set; }
    public string? CVV { get; set; }
    public string? CardHolder { get; set; }
}

public class PaymentReceiptDto
{
    public string TransactionId { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public string? CardBrand { get; set; }
    public string? CardLastFour { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public string Status { get; set; } = string.Empty;
    public DateTime? PaidAt { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class PaymentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public string? TransactionId { get; set; }
    public string? CardLastFour { get; set; }
    public string? CardBrand { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? FailureReason { get; set; }
    public DateTime CreatedAt { get; set; }
}
