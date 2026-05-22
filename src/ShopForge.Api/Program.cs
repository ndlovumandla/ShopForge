using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ShopForge.Api.Extensions;
using ShopForge.Api.Hubs;
using ShopForge.Api.Middleware;
using ShopForge.Database;
using ShopForge.Api.Validators;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    ValidateStartupConfiguration(builder.Configuration, builder.Environment);

    builder.Host.UseSerilog((ctx, services, cfg) =>
        cfg.ReadFrom.Configuration(ctx.Configuration)
           .ReadFrom.Services(services)
           .Enrich.FromLogContext()
           .WriteTo.Console());

    builder.Services.AddDatabase(builder.Configuration);
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
    builder.Services.AddSignalR();
    builder.Services.AddSwaggerWithJwt();
    builder.Services.AddCorsPolicy(builder.Configuration);
    builder.Services.AddApiRateLimiting();
    builder.Services.AddHealthChecks().AddDbContextCheck<ShopForgeDbContext>("database");

    builder.Services.AddControllers()
        .AddJsonOptions(o => o.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddMemoryCache();

    var app = builder.Build();

    var webRoot = app.Environment.WebRootPath ?? Path.Combine(app.Environment.ContentRootPath, "wwwroot");
    Directory.CreateDirectory(Path.Combine(webRoot, "uploads", "products"));

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopForge API v1"));
    }

    app.UseStaticFiles();
    app.UseHttpsRedirection();
    app.UseCors("AllowFrontend");
    app.UseRateLimiter();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<AuditLoggingMiddleware>();

    app.MapControllers().RequireRateLimiting("api-default");
    app.MapHub<OrderHub>("/hubs/orders");
    app.MapHealthChecks("/health/live");
    app.MapHealthChecks("/health/ready");

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "ShopForge API failed to start.");
}
finally
{
    Log.CloseAndFlush();
}

static void ValidateStartupConfiguration(IConfiguration config, IWebHostEnvironment env)
{
    var errors = new List<string>();

    if (string.IsNullOrWhiteSpace(config.GetConnectionString("DefaultConnection")))
        errors.Add("ConnectionStrings:DefaultConnection is required.");

    var jwtKey = config["Jwt:Key"];
    if (string.IsNullOrWhiteSpace(jwtKey))
    {
        errors.Add("Jwt:Key is required. Set it with environment variable JWT__Key or a secret store.");
    }
    else
    {
        if (jwtKey.Length < 32)
            errors.Add("Jwt:Key must be at least 32 characters long.");

        if (!env.IsDevelopment() && (jwtKey.Contains("__SET_IN_ENV__") || jwtKey.Contains("DEV-ONLY")))
            errors.Add("Jwt:Key is using a placeholder value outside Development.");
    }

    if (string.IsNullOrWhiteSpace(config["Jwt:Issuer"]))
        errors.Add("Jwt:Issuer is required.");

    if (string.IsNullOrWhiteSpace(config["Jwt:Audience"]))
        errors.Add("Jwt:Audience is required.");

    if (errors.Count > 0)
        throw new InvalidOperationException($"Startup configuration is invalid:{Environment.NewLine}- {string.Join(Environment.NewLine + "- ", errors)}");
}
