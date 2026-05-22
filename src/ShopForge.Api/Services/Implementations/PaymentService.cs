using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Hubs;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Payments;

namespace ShopForge.Api.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly ShopForgeDbContext _db;
    private readonly IHubContext<OrderHub> _hub;
    private readonly INotificationService _notifications;

    public PaymentService(ShopForgeDbContext db, IHubContext<OrderHub> hub, INotificationService notifications)
    {
        _db = db;
        _hub = hub;
        _notifications = notifications;
    }

    public async Task<ApiResponse<PaymentReceiptDto>> ProcessPaymentAsync(ProcessPaymentRequest request, int userId)
    {
        var order = await _db.Orders
            .Include(o => o.Items).ThenInclude(i => i.Product)
            .Include(o => o.Items).ThenInclude(i => i.ProductVariant)
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.UserId == userId);

        if (order == null) return ApiResponse<PaymentReceiptDto>.Fail("Order not found.");
        if (order.Payment?.Status == "Completed") return ApiResponse<PaymentReceiptDto>.Fail("Order is already paid.");
        if (order.Status == "Cancelled") return ApiResponse<PaymentReceiptDto>.Fail("Cannot pay for a cancelled order.");

        // Create or reuse payment record
        var payment = order.Payment ?? new Payment
        {
            OrderId = order.Id,
            Method = request.Method,
            Amount = order.TotalAmount,
            Currency = "ZAR",
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (order.Payment == null)
            _db.Payments.Add(payment);

        if (request.Method == "MockCard")
        {
            await Task.Delay(1500); // Simulate processing delay

            var cardNumber = (request.CardNumber ?? "").Replace(" ", "").Replace("-", "");
            var (isValid, brandName, errorMessage) = ValidateMockCard(request, cardNumber);

            if (!isValid)
            {
                payment.Status = "Failed";
                payment.FailureReason = errorMessage;
                payment.UpdatedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                return ApiResponse<PaymentReceiptDto>.Fail(errorMessage ?? "Payment declined.");
            }

            var last4 = cardNumber.Length >= 4 ? cardNumber[^4..] : cardNumber;

            // Check outcome based on last 4 digits
            string? declineReason = last4 switch
            {
                AppConstants.PaymentOutcomes.DeclineInsufficientFunds => "Declined: Insufficient funds.",
                AppConstants.PaymentOutcomes.DeclineCardExpired => "Declined: Card has expired.",
                AppConstants.PaymentOutcomes.DeclineFraudSuspected => "Declined: Fraud suspected.",
                _ => null
            };

            if (declineReason != null)
            {
                payment.Status = "Failed";
                payment.FailureReason = declineReason;
                payment.UpdatedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                return ApiResponse<PaymentReceiptDto>.Fail(declineReason);
            }

            // Payment success
            payment.Status = "Completed";
            payment.TransactionId = $"TXN-{Guid.NewGuid():N}".ToUpper()[..20];
            payment.CardLastFour = last4;
            payment.CardBrand = brandName;
            payment.PaidAt = DateTime.UtcNow;
            payment.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            payment.Status = "Failed";
            payment.FailureReason = "Unsupported payment method.";
            payment.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return ApiResponse<PaymentReceiptDto>.Fail("Unsupported payment method.");
        }

        // Update order status
        order.Status = "Confirmed";
        order.UpdatedAt = DateTime.UtcNow;

        _db.OrderStatusHistories.Add(new OrderStatusHistory
        {
            OrderId = order.Id,
            Status = "Confirmed",
            Note = "Payment confirmed.",
            CreatedAt = DateTime.UtcNow
        });

        // Deduct stock
        foreach (var item in order.Items)
        {
            if (item.ProductVariantId.HasValue && item.ProductVariant != null)
            {
                item.ProductVariant.StockQuantity = Math.Max(0, item.ProductVariant.StockQuantity - item.Quantity);
            }
            else if (item.Product != null)
            {
                item.Product.StockQuantity = Math.Max(0, item.Product.StockQuantity - item.Quantity);
            }

            _db.InventoryLogs.Add(new InventoryLog
            {
                ProductId = item.ProductId,
                ProductVariantId = item.ProductVariantId,
                ChangeAmount = -item.Quantity,
                Reason = "SaleOrder",
                ReferenceId = order.Id,
                Note = $"Deducted for order {order.OrderNumber}",
                CreatedAt = DateTime.UtcNow
            });
        }

        await _db.SaveChangesAsync();

        // Create notification
        await _notifications.CreateNotificationAsync(
            userId,
            "Order Confirmed",
            $"Your order {order.OrderNumber} has been confirmed.",
            "OrderConfirmed",
            $"/orders/{order.Id}"
        );

        // Broadcast to admins via SignalR
        await _hub.Clients.Group("admins").SendAsync("NewOrder", new
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            Amount = order.TotalAmount,
            Status = "Confirmed"
        });

        return ApiResponse<PaymentReceiptDto>.Ok(new PaymentReceiptDto
        {
            TransactionId = payment.TransactionId ?? string.Empty,
            OrderNumber = order.OrderNumber,
            CardBrand = payment.CardBrand,
            CardLastFour = payment.CardLastFour,
            Amount = payment.Amount,
            Currency = payment.Currency,
            Status = "Completed",
            PaidAt = payment.PaidAt,
            Message = "Payment successful."
        });
    }

    public async Task<ApiResponse<PaymentDto>> GetPaymentByOrderIdAsync(int orderId)
    {
        var payment = await _db.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
        if (payment == null) return ApiResponse<PaymentDto>.Fail("Payment not found.");

        return ApiResponse<PaymentDto>.Ok(new PaymentDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Method = payment.Method,
            Status = payment.Status,
            Amount = payment.Amount,
            Currency = payment.Currency,
            TransactionId = payment.TransactionId,
            CardLastFour = payment.CardLastFour,
            CardBrand = payment.CardBrand,
            PaidAt = payment.PaidAt,
            FailureReason = payment.FailureReason,
            CreatedAt = payment.CreatedAt
        });
    }

    public async Task<ApiResponse<bool>> RefundPaymentAsync(int orderId)
    {
        var order = await _db.Orders
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return ApiResponse<bool>.Fail("Order not found.");
        }

        if (order.Payment == null)
        {
            return ApiResponse<bool>.Fail("Payment not found for this order.");
        }

        if (!string.Equals(order.Payment.Status, "Completed", StringComparison.OrdinalIgnoreCase))
        {
            return ApiResponse<bool>.Fail("Only completed payments can be refunded.");
        }

        order.Payment.Status = "Refunded";
        order.Payment.UpdatedAt = DateTime.UtcNow;

        order.Status = "Refunded";
        order.UpdatedAt = DateTime.UtcNow;

        _db.OrderStatusHistories.Add(new OrderStatusHistory
        {
            OrderId = order.Id,
            Status = "Refunded",
            Note = "Refund processed by admin.",
            CreatedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Payment refunded successfully.");
    }

    private static (bool IsValid, string? Brand, string? Error) ValidateMockCard(ProcessPaymentRequest req, string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 13)
            return (false, null, "Invalid card number.");

        if (!IsValidLuhn(cardNumber))
            return (false, null, "Invalid card number.");

        var now = DateTime.UtcNow;
        if (!req.ExpiryMonth.HasValue || !req.ExpiryYear.HasValue)
            return (false, null, "Expiry date required.");

        var expiryYear = req.ExpiryYear.Value < 100 ? 2000 + req.ExpiryYear.Value : req.ExpiryYear.Value;
        var expiryDate = new DateTime(expiryYear, req.ExpiryMonth.Value, 1).AddMonths(1).AddDays(-1);

        if (expiryDate < now)
            return (false, null, "Card has expired.");

        if (string.IsNullOrEmpty(req.CVV) || req.CVV.Length < 3)
            return (false, null, "Invalid CVV.");

        var brand = DetectCardBrand(cardNumber);
        return (true, brand, null);
    }

    private static bool IsValidLuhn(string number)
    {
        int sum = 0;
        bool alternate = false;
        for (int i = number.Length - 1; i >= 0; i--)
        {
            if (!char.IsDigit(number[i])) return false;
            int digit = number[i] - '0';
            if (alternate)
            {
                digit *= 2;
                if (digit > 9) digit -= 9;
            }
            sum += digit;
            alternate = !alternate;
        }
        return sum % 10 == 0;
    }

    private static string DetectCardBrand(string number) =>
        number[0] switch
        {
            '4' => "Visa",
            '5' => "Mastercard",
            '3' when number.Length >= 2 && (number[1] == '4' || number[1] == '7') => "AmericanExpress",
            '6' => "Discover",
            _ => "Unknown"
        };
}
