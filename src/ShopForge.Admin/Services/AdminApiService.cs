using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Auth;
using ShopForge.Shared.DTOs.Categories;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Orders;
using ShopForge.Shared.DTOs.Products;
using ShopForge.Shared.DTOs.Reports;

namespace ShopForge.Admin.Services;

public class AdminApiService : IAdminApiService
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };
    private const long MaxProductImageUploadBytes = 5 * 1024 * 1024;

    public AdminApiService(HttpClient http) => _http = http;

    public void SetToken(string token) =>
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    private async Task<T?> GetAsync<T>(string url)
    {
        var r = await _http.GetAsync(url);
        if (!r.IsSuccessStatusCode) return default;
        return JsonSerializer.Deserialize<T>(await r.Content.ReadAsStringAsync(), _json);
    }

    private async Task<T?> PostAsync<T>(string url, object? body = null)
    {
        var content = body != null ? new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json") : null;
        var r = await _http.PostAsync(url, content);
        var s = await r.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(s) ? default : JsonSerializer.Deserialize<T>(s, _json);
    }

    private async Task<T?> PutAsync<T>(string url, object body)
    {
        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        var r = await _http.PutAsync(url, content);
        var s = await r.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(s) ? default : JsonSerializer.Deserialize<T>(s, _json);
    }

    private async Task<T?> DeleteAsync<T>(string url)
    {
        var r = await _http.DeleteAsync(url);
        var s = await r.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(s) ? default : JsonSerializer.Deserialize<T>(s, _json);
    }

    private async Task<T?> PatchAsync<T>(string url, object? body = null)
    {
        var content = body != null ? new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json") : null;
        var req = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
        var r = await _http.SendAsync(req);
        var s = await r.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(s) ? default : JsonSerializer.Deserialize<T>(s, _json);
    }

    public Task<ApiResponse<AuthResponse>?> LoginAsync(LoginRequest request) =>
        PostAsync<ApiResponse<AuthResponse>>("api/auth/login", request);

    public Task<ApiResponse<bool>?> LogoutAsync(string refreshToken) =>
        PostAsync<ApiResponse<bool>>("api/auth/logout", new { refreshToken });

    public Task<ApiResponse<UserProfileDto>?> GetProfileAsync() =>
        GetAsync<ApiResponse<UserProfileDto>>("api/auth/me");

    public Task<ApiResponse<UserProfileDto>?> UpdateProfileAsync(UpdateProfileRequest request) =>
        PutAsync<ApiResponse<UserProfileDto>>("api/auth/me", request);

    public Task<ApiResponse<bool>?> ChangePasswordAsync(ChangePasswordRequest request) =>
        PutAsync<ApiResponse<bool>>("api/auth/me/password", request);

    public Task<ApiResponse<DashboardSummaryDto>?> GetDashboardAsync() =>
        GetAsync<ApiResponse<DashboardSummaryDto>>("api/admin/reports/dashboard");

    public Task<ApiResponse<PagedResult<ProductSummaryDto>>?> GetProductsAsync(int page = 1, int pageSize = 20, string? search = null, int? categoryId = null)
    {
        var qs = $"api/admin/products?page={page}&pageSize={pageSize}";
        if (!string.IsNullOrEmpty(search)) qs += $"&search={Uri.EscapeDataString(search)}";
        if (categoryId.HasValue) qs += $"&categoryId={categoryId}";
        return GetAsync<ApiResponse<PagedResult<ProductSummaryDto>>>(qs);
    }

    public Task<ApiResponse<ProductDto>?> GetProductAsync(int id) =>
        GetAsync<ApiResponse<ProductDto>>($"api/admin/products/{id}");
    public Task<ApiResponse<ProductDto>?> CreateProductAsync(CreateProductRequest request) =>
        PostAsync<ApiResponse<ProductDto>>("api/admin/products", request);
    public Task<ApiResponse<ProductDto>?> UpdateProductAsync(int id, UpdateProductRequest request) =>
        PutAsync<ApiResponse<ProductDto>>($"api/admin/products/{id}", request);
    public Task<ApiResponse<bool>?> DeleteProductAsync(int id) =>
        DeleteAsync<ApiResponse<bool>>($"api/admin/products/{id}");
    public Task<ApiResponse<bool>?> AdjustStockAsync(int id, int adjustment, string reason) =>
        PostAsync<ApiResponse<bool>>($"api/admin/products/{id}/stock", new { changeAmount = adjustment, reason });

    public async Task<ApiResponse<ProductImageUploadResult>?> UploadProductImageAsync(IBrowserFile file)
    {
        using var form = new MultipartFormDataContent();
        await using var stream = file.OpenReadStream(MaxProductImageUploadBytes);
        using var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        form.Add(fileContent, "file", file.Name);

        var response = await _http.PostAsync("api/admin/uploads/product-images", form);
        var body = await response.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(body)
            ? default
            : JsonSerializer.Deserialize<ApiResponse<ProductImageUploadResult>>(body, _json);
    }

    public Task<ApiResponse<List<CategoryDto>>?> GetCategoriesAsync() =>
        GetAsync<ApiResponse<List<CategoryDto>>>("api/admin/products/categories");
    public Task<ApiResponse<CategoryDto>?> CreateCategoryAsync(CreateCategoryRequest request) =>
        PostAsync<ApiResponse<CategoryDto>>("api/admin/products/categories", request);
    public Task<ApiResponse<CategoryDto>?> UpdateCategoryAsync(int id, CreateCategoryRequest request) =>
        PutAsync<ApiResponse<CategoryDto>>($"api/admin/products/categories/{id}", request);
    public Task<ApiResponse<bool>?> DeleteCategoryAsync(int id) =>
        DeleteAsync<ApiResponse<bool>>($"api/admin/products/categories/{id}");

    public Task<ApiResponse<PagedResult<OrderSummaryDto>>?> GetOrdersAsync(int page = 1, int pageSize = 20, string? search = null, string? status = null, DateTime? from = null, DateTime? to = null)
    {
        var qs = $"api/admin/orders?page={page}&pageSize={pageSize}";
        if (!string.IsNullOrEmpty(search)) qs += $"&search={Uri.EscapeDataString(search)}";
        if (!string.IsNullOrEmpty(status)) qs += $"&status={status}";
        if (from.HasValue) qs += $"&from={from:yyyy-MM-dd}";
        if (to.HasValue) qs += $"&to={to:yyyy-MM-dd}";
        return GetAsync<ApiResponse<PagedResult<OrderSummaryDto>>>(qs);
    }
    public Task<ApiResponse<OrderDto>?> GetOrderAsync(int id) =>
        GetAsync<ApiResponse<OrderDto>>($"api/admin/orders/{id}");
    public Task<ApiResponse<OrderDto>?> UpdateOrderStatusAsync(int id, UpdateOrderStatusRequest request) =>
        PutAsync<ApiResponse<OrderDto>>($"api/admin/orders/{id}/status", request);
    public Task<ApiResponse<OrderDto>?> SetTrackingNumberAsync(int id, string trackingNumber) =>
        PutAsync<ApiResponse<OrderDto>>($"api/admin/orders/{id}/tracking", new { trackingNumber });
    public Task<ApiResponse<bool>?> RefundOrderAsync(int id) =>
        PostAsync<ApiResponse<bool>>($"api/payments/{id}/refund", new { });

    public Task<ApiResponse<PagedResult<AdminUserDto>>?> GetCustomersAsync(int page = 1, int pageSize = 20, string? search = null) =>
        GetAsync<ApiResponse<PagedResult<AdminUserDto>>>($"api/admin/users?page={page}&pageSize={pageSize}{(string.IsNullOrEmpty(search) ? "" : $"&search={Uri.EscapeDataString(search)}")}");
    public Task<ApiResponse<AdminUserDto>?> GetCustomerAsync(int id) =>
        GetAsync<ApiResponse<AdminUserDto>>($"api/admin/users/{id}");
    public Task<ApiResponse<AuthResponse>?> RegisterCustomerAsync(RegisterRequest request) =>
        PostAsync<ApiResponse<AuthResponse>>("api/auth/register", request);
    public Task<ApiResponse<bool>?> ToggleCustomerActiveAsync(int id) =>
        PutAsync<ApiResponse<bool>>($"api/admin/users/{id}/toggle-active", new { });
    public Task<ApiResponse<bool>?> ChangeCustomerRoleAsync(int id, string role) =>
        PutAsync<ApiResponse<bool>>($"api/admin/users/{id}/role", new { role });

    public Task<ApiResponse<List<CouponDto>>?> GetCouponsAsync() =>
        GetAsync<ApiResponse<List<CouponDto>>>("api/admin/coupons");
    public Task<ApiResponse<CouponDto>?> CreateCouponAsync(CreateCouponRequest request) =>
        PostAsync<ApiResponse<CouponDto>>("api/admin/coupons", request);
    public Task<ApiResponse<CouponDto>?> UpdateCouponAsync(int id, CreateCouponRequest request) =>
        PutAsync<ApiResponse<CouponDto>>($"api/admin/coupons/{id}", request);
    public Task<ApiResponse<bool>?> DeleteCouponAsync(int id) =>
        DeleteAsync<ApiResponse<bool>>($"api/admin/coupons/{id}");

    public Task<ApiResponse<List<SalesDataPointDto>>?> GetSalesReportAsync(DateTime from, DateTime to, string groupBy = "day") =>
        GetAsync<ApiResponse<List<SalesDataPointDto>>>($"api/admin/reports/sales?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}&groupBy={groupBy}");
    public Task<ApiResponse<List<RevenueDataPoint>>?> GetRevenueByCategoryAsync(DateTime from, DateTime to) =>
        GetAsync<ApiResponse<List<RevenueDataPoint>>>($"api/admin/reports/revenue-by-category?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
    public Task<ApiResponse<List<TopProductDto>>?> GetTopProductsAsync(DateTime from, DateTime to, string by = "revenue") =>
        GetAsync<ApiResponse<List<TopProductDto>>>($"api/admin/reports/top-products?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}&by={by}");
    public Task<ApiResponse<List<InventoryReportItemDto>>?> GetInventoryReportAsync() =>
        GetAsync<ApiResponse<List<InventoryReportItemDto>>>("api/admin/reports/inventory");

    public Task<ApiResponse<List<BannerSlideDto>>?> GetBannersAsync() =>
        GetAsync<ApiResponse<List<BannerSlideDto>>>("api/admin/settings/banners");
    public Task<ApiResponse<BannerSlideDto>?> CreateBannerAsync(BannerSlideDto banner) =>
        PostAsync<ApiResponse<BannerSlideDto>>("api/admin/settings/banners", banner);
    public Task<ApiResponse<BannerSlideDto>?> UpdateBannerAsync(int id, BannerSlideDto banner)
    {
        banner.Id = id;
        return PostAsync<ApiResponse<BannerSlideDto>>("api/admin/settings/banners", banner);
    }
    public Task<ApiResponse<bool>?> DeleteBannerAsync(int id) =>
        DeleteAsync<ApiResponse<bool>>($"api/admin/settings/banners/{id}");

    public Task<ApiResponse<List<AppSettingDto>>?> GetSettingsAsync() =>
        GetAsync<ApiResponse<List<AppSettingDto>>>("api/admin/settings");
    public Task<ApiResponse<bool>?> UpdateSettingsAsync(UpdateSettingsRequest request) =>
        PutAsync<ApiResponse<bool>>("api/admin/settings", request);
}
