using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ShopForge.Shared.Constants;

namespace ShopForge.Api.Hubs;

[Authorize]
public class OrderHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var role = Context.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        if (role is AppConstants.Roles.Admin or AppConstants.Roles.Manager)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "admins");
        }

        var userId = Context.User?.FindFirst(AppConstants.JwtClaims.UserId)?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
