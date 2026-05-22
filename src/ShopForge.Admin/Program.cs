using MudBlazor.Services;
using ShopForge.Admin.Components;
using ShopForge.Admin.Services;
using ShopForge.Admin.Hubs;
using ShopForge.Shared.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient<IAdminApiService, AdminApiService>(client =>
{
    var apiBase = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5000";
    client.BaseAddress = new Uri(apiBase);
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<IAdminAuthService, AdminAuthService>();
builder.Services.AddScoped<OrderHubConnection>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseSession();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPost("/auth/session", (AdminSessionRequest request, HttpContext context, HttpResponse response) =>
{
    if (string.IsNullOrWhiteSpace(request.Token) ||
        string.IsNullOrWhiteSpace(request.RefreshToken) ||
        request.UserId <= 0 ||
        string.IsNullOrWhiteSpace(request.UserName) ||
        string.IsNullOrWhiteSpace(request.Role))
    {
        return Results.BadRequest();
    }

    if (!string.Equals(request.Role, UserRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase) &&
        !string.Equals(request.Role, UserRole.Manager.ToString(), StringComparison.OrdinalIgnoreCase))
    {
        return Results.Forbid();
    }

    var options = new CookieOptions
    {
        HttpOnly = true,
        IsEssential = true,
        SameSite = SameSiteMode.Lax,
        Secure = context.Request.IsHttps,
        Expires = DateTimeOffset.UtcNow.AddHours(8)
    };

    response.Cookies.Append("admin_token", request.Token, options);
    response.Cookies.Append("admin_refresh_token", request.RefreshToken, options);
    response.Cookies.Append("admin_user_id", request.UserId.ToString(), options);
    response.Cookies.Append("admin_user", request.UserName, options);
    response.Cookies.Append("admin_role", request.Role, options);

    return Results.Ok();
}).DisableAntiforgery();

app.MapDelete("/auth/session", (HttpResponse response) =>
{
    response.Cookies.Delete("admin_token");
    response.Cookies.Delete("admin_refresh_token");
    response.Cookies.Delete("admin_user_id");
    response.Cookies.Delete("admin_user");
    response.Cookies.Delete("admin_role");

    return Results.Ok();
}).DisableAntiforgery();

app.Run();

record AdminSessionRequest(string Token, string RefreshToken, int UserId, string UserName, string Role);
