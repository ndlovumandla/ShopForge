using System.Security.Claims;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.Constants;

namespace ShopForge.Api.Middleware;

public class AuditLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public AuditLoggingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, ShopForgeDbContext db)
    {
        await _next(context);

        var method = context.Request.Method;
        if (method is "GET" or "HEAD" or "OPTIONS") return;

        var userId = context.User.FindFirstValue(AppConstants.JwtClaims.UserId);
        if (string.IsNullOrEmpty(userId)) return;

        if (!int.TryParse(userId, out var uid)) return;

        db.AuditLogs.Add(new AuditLog
        {
            UserId = uid,
            Action = method,
            EntityType = context.Request.Path,
            EntityId = null,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers.UserAgent.ToString(),
            CreatedAt = DateTime.UtcNow
        });

        await db.SaveChangesAsync();
    }
}
