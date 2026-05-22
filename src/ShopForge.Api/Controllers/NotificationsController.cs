using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notifications;

    public NotificationsController(INotificationService notifications) => _notifications = notifications;

    private int GetUserId() => int.Parse(User.FindFirstValue(AppConstants.JwtClaims.UserId)!);

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<NotificationDto>>>> Get([FromQuery] bool unreadOnly = false)
        => Ok(await _notifications.GetUserNotificationsAsync(GetUserId(), unreadOnly));

    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> GetUnreadCount()
        => Ok(ApiResponse<int>.Ok(await _notifications.GetUnreadCountAsync(GetUserId())));

    [HttpPut("{id:int}/read")]
    public async Task<ActionResult<ApiResponse<bool>>> MarkRead(int id)
    {
        var result = await _notifications.MarkAsReadAsync(id, GetUserId());
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("read-all")]
    public async Task<ActionResult<ApiResponse<bool>>> MarkAllRead()
        => Ok(await _notifications.MarkAllAsReadAsync(GetUserId()));

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _notifications.DeleteNotificationAsync(id, GetUserId());
        return result.Success ? Ok(result) : NotFound(result);
    }
}
