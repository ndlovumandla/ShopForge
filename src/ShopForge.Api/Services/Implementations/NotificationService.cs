using Microsoft.EntityFrameworkCore;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Implementations;

public class NotificationService : INotificationService
{
    private readonly ShopForgeDbContext _db;

    public NotificationService(ShopForgeDbContext db) => _db = db;

    public async Task<ApiResponse<List<NotificationDto>>> GetUserNotificationsAsync(int userId, bool unreadOnly = false)
    {
        var query = _db.Notifications.Where(n => n.UserId == userId);
        if (unreadOnly) query = query.Where(n => !n.IsRead);

        var items = await query.OrderByDescending(n => n.CreatedAt).Take(50).ToListAsync();
        return ApiResponse<List<NotificationDto>>.Ok(items.Select(MapToDto).ToList());
    }

    public async Task<ApiResponse<bool>> MarkAsReadAsync(int notificationId, int userId)
    {
        var notification = await _db.Notifications.FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
        if (notification == null) return ApiResponse<bool>.Fail("Notification not found.");
        notification.IsRead = true;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<bool>> MarkAllAsReadAsync(int userId)
    {
        var notifications = await _db.Notifications.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();
        notifications.ForEach(n => n.IsRead = true);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<bool>> DeleteNotificationAsync(int notificationId, int userId)
    {
        var notification = await _db.Notifications.FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
        if (notification == null) return ApiResponse<bool>.Fail("Notification not found.");
        _db.Notifications.Remove(notification);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<int> GetUnreadCountAsync(int userId) =>
        await _db.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);

    public async Task CreateNotificationAsync(int userId, string title, string message, string type, string? actionUrl = null)
    {
        _db.Notifications.Add(new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            Type = type,
            ActionUrl = actionUrl,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
    }

    private static NotificationDto MapToDto(Notification n) => new()
    {
        Id = n.Id,
        Title = n.Title,
        Message = n.Message,
        Type = n.Type,
        IsRead = n.IsRead,
        ActionUrl = n.ActionUrl,
        CreatedAt = n.CreatedAt
    };
}
