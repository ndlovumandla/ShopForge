using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Interfaces;

public interface INotificationService
{
    Task<ApiResponse<List<NotificationDto>>> GetUserNotificationsAsync(int userId, bool unreadOnly = false);
    Task<ApiResponse<bool>> MarkAsReadAsync(int notificationId, int userId);
    Task<ApiResponse<bool>> MarkAllAsReadAsync(int userId);
    Task<ApiResponse<bool>> DeleteNotificationAsync(int notificationId, int userId);
    Task<int> GetUnreadCountAsync(int userId);
    Task CreateNotificationAsync(int userId, string title, string message, string type, string? actionUrl = null);
}
