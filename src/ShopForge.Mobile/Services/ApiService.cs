using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ShopForge.Shared.DTOs.Admin;
using ShopForge.Shared.DTOs.Auth;
using ShopForge.Shared.DTOs.Cart;
using ShopForge.Shared.DTOs.Categories;
using ShopForge.Shared.DTOs.Common;
using ShopForge.Shared.DTOs.Orders;
using ShopForge.Shared.DTOs.Payments;
using ShopForge.Shared.DTOs.Products;
using ShopForge.Shared.DTOs.Reviews;

namespace ShopForge.Mobile.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _http;
    private readonly IAuthStateService _authState;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public ApiService(HttpClient http, IAuthStateService authState)
    {
        _http = http;
        _authState = authState;
    }

    private void AttachToken()
    {
        var token = _authState.AccessToken;
        if (!string.IsNullOrEmpty(token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return;
        }

        _http.DefaultRequestHeaders.Authorization = null;
    }

    private async Task<T?> GetAsync<T>(string url)
    {
        AttachToken();
        var response = await _http.GetAsync(url);
        if (!response.IsSuccessStatusCode) return default;
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, _json);
    }

    private async Task<T?> PostAsync<T>(string url, object body)
    {
        AttachToken();
        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseContent)) return default;
        return JsonSerializer.Deserialize<T>(responseContent, _json);
    }

    private async Task<T?> PutAsync<T>(string url, object body)
    {
        AttachToken();
        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        var response = await _http.PutAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseContent)) return default;
        return JsonSerializer.Deserialize<T>(responseContent, _json);
    }

    private async Task<T?> DeleteAsync<T>(string url)
    {
        AttachToken();
        var response = await _http.DeleteAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseContent)) return default;
        return JsonSerializer.Deserialize<T>(responseContent, _json);
    }

    private async Task<T?> PatchAsync<T>(string url, object? body = null)
    {
        AttachToken();
        HttpContent? content = body != null
            ? new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            : null;
        var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
        var response = await _http.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseContent)) return default;
        return JsonSerializer.Deserialize<T>(responseContent, _json);
    }

    public Task<ApiResponse<AuthResponse>?> LoginAsync(LoginRequest request) => PostAsync<ApiResponse<AuthResponse>>("api/auth/login", request);
    public Task<ApiResponse<AuthResponse>?> RegisterAsync(RegisterRequest request) => PostAsync<ApiResponse<AuthResponse>>("api/auth/register", request);
    public Task<ApiResponse<AuthResponse>?> RefreshTokenAsync(string refreshToken) => PostAsync<ApiResponse<AuthResponse>>("api/auth/refresh", new { refreshToken });
    public Task<ApiResponse<bool>?> LogoutAsync(string refreshToken) => PostAsync<ApiResponse<bool>>("api/auth/logout", new { refreshToken });
    public Task<ApiResponse<UserProfileDto>?> GetProfileAsync() => GetAsync<ApiResponse<UserProfileDto>>("api/auth/me");
    public Task<ApiResponse<UserProfileDto>?> UpdateProfileAsync(object request) => PutAsync<ApiResponse<UserProfileDto>>("api/auth/me", request);
    public Task<ApiResponse<bool>?> ChangePasswordAsync(object request) => PutAsync<ApiResponse<bool>>("api/auth/me/password", request);
    public Task<ApiResponse<bool>?> ForgotPasswordAsync(string email) => PostAsync<ApiResponse<bool>>("api/auth/forgot-password", new { email });

    public Task<ApiResponse<PagedResult<ProductSummaryDto>>?> GetProductsAsync(int page = 1, int pageSize = 20, int? categoryId = null, int? brandId = null, decimal? minPrice = null, decimal? maxPrice = null, string? search = null, string? sort = null, bool? featured = null)
    {
        var qs = $"api/products?page={page}&pageSize={pageSize}";
        if (categoryId.HasValue) qs += $"&categoryId={categoryId}";
        if (brandId.HasValue) qs += $"&brandId={brandId}";
        if (minPrice.HasValue) qs += $"&minPrice={minPrice}";
        if (maxPrice.HasValue) qs += $"&maxPrice={maxPrice}";
        if (!string.IsNullOrEmpty(search)) qs += $"&search={Uri.EscapeDataString(search)}";
        if (!string.IsNullOrEmpty(sort)) qs += $"&sort={sort}";
        if (featured.HasValue) qs += $"&featured={featured.Value}";
        return GetAsync<ApiResponse<PagedResult<ProductSummaryDto>>>(qs);
    }

    public Task<ApiResponse<ProductDto>?> GetProductByIdAsync(int id) => GetAsync<ApiResponse<ProductDto>>($"api/products/{id}");
    public Task<ApiResponse<ProductDto>?> GetProductBySlugAsync(string slug) => GetAsync<ApiResponse<ProductDto>>($"api/products/slug/{slug}");
    public Task<ApiResponse<List<ProductSummaryDto>>?> GetFeaturedProductsAsync() => GetAsync<ApiResponse<List<ProductSummaryDto>>>("api/products/featured");
    public Task<ApiResponse<List<ProductSummaryDto>>?> GetNewArrivalsAsync() => GetAsync<ApiResponse<List<ProductSummaryDto>>>("api/products/new-arrivals");
    public Task<ApiResponse<PagedResult<ProductSummaryDto>>?> SearchProductsAsync(string query, int page = 1, int pageSize = 20) => GetAsync<ApiResponse<PagedResult<ProductSummaryDto>>>($"api/products/search?q={Uri.EscapeDataString(query)}&page={page}&pageSize={pageSize}");
    public Task<ApiResponse<PagedResult<ReviewDto>>?> GetProductReviewsAsync(int productId, int page = 1, int pageSize = 10) => GetAsync<ApiResponse<PagedResult<ReviewDto>>>($"api/products/{productId}/reviews?page={page}&pageSize={pageSize}");
    public Task<ApiResponse<ReviewDto>?> CreateReviewAsync(int productId, CreateReviewRequest request) => PostAsync<ApiResponse<ReviewDto>>($"api/products/{productId}/reviews", request);
    public Task<ApiResponse<List<CategoryDto>>?> GetCategoriesAsync() => GetAsync<ApiResponse<List<CategoryDto>>>("api/categories");
    public Task<ApiResponse<CartDto>?> GetCartAsync() => GetAsync<ApiResponse<CartDto>>("api/cart");
    public Task<ApiResponse<CartDto>?> AddToCartAsync(AddToCartRequest request) => PostAsync<ApiResponse<CartDto>>("api/cart/items", request);
    public Task<ApiResponse<CartDto>?> UpdateCartItemAsync(int itemId, UpdateCartItemRequest request) => PutAsync<ApiResponse<CartDto>>($"api/cart/items/{itemId}", request);
    public Task<ApiResponse<bool>?> RemoveCartItemAsync(int itemId) => DeleteAsync<ApiResponse<bool>>($"api/cart/items/{itemId}");
    public Task<ApiResponse<bool>?> ClearCartAsync() => DeleteAsync<ApiResponse<bool>>("api/cart");
    public Task<ApiResponse<CartDto>?> ApplyCouponAsync(string couponCode) => PostAsync<ApiResponse<CartDto>>("api/cart/coupon", new { couponCode });
    public Task<ApiResponse<CartDto>?> RemoveCouponAsync() => DeleteAsync<ApiResponse<CartDto>>("api/cart/coupon");
    public Task<ApiResponse<List<ProductSummaryDto>>?> GetWishlistAsync() => GetAsync<ApiResponse<List<ProductSummaryDto>>>("api/wishlist");
    public Task<ApiResponse<bool>?> AddToWishlistAsync(int productId) => PostAsync<ApiResponse<bool>>($"api/wishlist/{productId}", new { });
    public Task<ApiResponse<bool>?> RemoveFromWishlistAsync(int productId) => DeleteAsync<ApiResponse<bool>>($"api/wishlist/{productId}");
    public Task<ApiResponse<bool>?> MoveToCartAsync(int productId) => PostAsync<ApiResponse<bool>>($"api/wishlist/{productId}/move-to-cart", new { });
    public Task<ApiResponse<PagedResult<OrderSummaryDto>>?> GetOrdersAsync(int page = 1, int pageSize = 20) => GetAsync<ApiResponse<PagedResult<OrderSummaryDto>>>($"api/orders?page={page}&pageSize={pageSize}");
    public Task<ApiResponse<OrderDto>?> GetOrderByIdAsync(int id) => GetAsync<ApiResponse<OrderDto>>($"api/orders/{id}");
    public Task<ApiResponse<OrderDto>?> CreateOrderAsync(CreateOrderRequest request) => PostAsync<ApiResponse<OrderDto>>("api/orders", request);
    public Task<ApiResponse<OrderDto>?> CancelOrderAsync(int id, string reason) => PostAsync<ApiResponse<OrderDto>>($"api/orders/{id}/cancel", new { reason });
    public Task<ApiResponse<PaymentReceiptDto>?> ProcessPaymentAsync(ProcessPaymentRequest request) => PostAsync<ApiResponse<PaymentReceiptDto>>("api/payments", request);
    public Task<ApiResponse<List<AddressDto>>?> GetAddressesAsync() => GetAsync<ApiResponse<List<AddressDto>>>("api/addresses");
    public Task<ApiResponse<AddressDto>?> CreateAddressAsync(AddressDto address) => PostAsync<ApiResponse<AddressDto>>("api/addresses", address);
    public Task<ApiResponse<AddressDto>?> UpdateAddressAsync(int id, AddressDto address) => PutAsync<ApiResponse<AddressDto>>($"api/addresses/{id}", address);
    public Task<ApiResponse<bool>?> DeleteAddressAsync(int id) => DeleteAsync<ApiResponse<bool>>($"api/addresses/{id}");
    public Task<ApiResponse<bool>?> SetDefaultAddressAsync(int id) => PatchAsync<ApiResponse<bool>>($"api/addresses/{id}/default");
    public Task<ApiResponse<List<NotificationDto>>?> GetNotificationsAsync() => GetAsync<ApiResponse<List<NotificationDto>>>("api/notifications");
    public Task<ApiResponse<bool>?> MarkNotificationReadAsync(int id) => PatchAsync<ApiResponse<bool>>($"api/notifications/{id}/read");
    public Task<ApiResponse<bool>?> MarkAllNotificationsReadAsync() => PutAsync<ApiResponse<bool>>("api/notifications/read-all", new { });
    public Task<ApiResponse<bool>?> DeleteNotificationAsync(int id) => DeleteAsync<ApiResponse<bool>>($"api/notifications/{id}");
    public Task<ApiResponse<List<BannerSlideDto>>?> GetBannersAsync() => GetAsync<ApiResponse<List<BannerSlideDto>>>("api/misc/banners");
    public Task<ApiResponse<List<ShippingMethodDto>>?> GetShippingMethodsAsync() => GetAsync<ApiResponse<List<ShippingMethodDto>>>("api/misc/shipping-methods");
    public Task<ApiResponse<CouponValidationResult>?> ValidateCouponAsync(string code, decimal cartTotal) => PostAsync<ApiResponse<CouponValidationResult>>("api/misc/validate-coupon", new { couponCode = code, cartTotal });
}
