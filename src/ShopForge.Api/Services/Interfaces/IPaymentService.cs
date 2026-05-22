using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Payments;

namespace ShopForge.Api.Services.Interfaces;

public interface IPaymentService
{
    Task<ApiResponse<PaymentReceiptDto>> ProcessPaymentAsync(ProcessPaymentRequest request, int userId);
    Task<ApiResponse<PaymentDto>> GetPaymentByOrderIdAsync(int orderId);
    Task<ApiResponse<bool>> RefundPaymentAsync(int orderId);
}
