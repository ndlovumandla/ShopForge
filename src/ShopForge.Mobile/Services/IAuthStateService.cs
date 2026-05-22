namespace ShopForge.Mobile.Services;

public interface IAuthStateService
{
    bool IsAuthenticated { get; }
    string? AccessToken { get; }
    string? RefreshToken { get; }
    string? UserName { get; }
    int UserId { get; }
    string? UserRole { get; }
    Task SetAuthAsync(string accessToken, string refreshToken, ShopForge.Shared.DTOs.Auth.UserProfileDto user);
    Task ClearAuthAsync();
}
