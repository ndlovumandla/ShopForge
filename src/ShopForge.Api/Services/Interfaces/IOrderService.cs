using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Orders;

namespace ShopForge.Api.Services.Interfaces;

public interface IOrderService
{
    Task<ApiResponse<OrderDto>> CreateOrderAsync(int userId, CreateOrderRequest request);
    Task<ApiResponse<OrderDto>> GetOrderByIdAsync(int orderId, int? userId = null);
    Task<ApiResponse<OrderDto>> GetOrderByNumberAsync(string orderNumber, int? userId = null);
    Task<ApiResponse<PagedResult<OrderSummaryDto>>> GetUserOrdersAsync(int userId, int page, int pageSize);
    Task<ApiResponse<PagedResult<OrderSummaryDto>>> GetAllOrdersAsync(int page, int pageSize, string? status, string? search);
    Task<ApiResponse<OrderDto>> UpdateStatusAsync(int orderId, UpdateOrderStatusRequest request);
    Task<ApiResponse<OrderDto>> CancelOrderAsync(int orderId, CancelOrderRequest request, int? userId = null);
    Task<ApiResponse<OrderDto>> SetTrackingNumberAsync(int orderId, SetTrackingRequest request);
}
