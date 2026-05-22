using ShopForge.Shared.DTOs.Auth;

namespace ShopForge.Mobile.Services;

public class AuthStateService : IAuthStateService
{
    public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);
    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public string? UserName { get; private set; }
    public int UserId { get; private set; }
    public string? UserRole { get; private set; }

    public AuthStateService()
    {
        AccessToken = SecureStorage.GetAsync("access_token").GetAwaiter().GetResult();
        RefreshToken = SecureStorage.GetAsync("refresh_token").GetAwaiter().GetResult();
        UserName = Preferences.Get("user_name", null as string);
        UserId = Preferences.Get("user_id", 0);
        UserRole = Preferences.Get("user_role", null as string);
    }

    public async Task SetAuthAsync(string accessToken, string refreshToken, UserProfileDto user)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UserName = $"{user.FirstName} {user.LastName}";
        UserId = user.Id;
        UserRole = user.Role;
        await SecureStorage.SetAsync("access_token", accessToken);
        await SecureStorage.SetAsync("refresh_token", refreshToken);
        Preferences.Set("user_name", UserName);
        Preferences.Set("user_id", UserId);
        Preferences.Set("user_role", UserRole ?? string.Empty);
    }

    public async Task ClearAuthAsync()
    {
        AccessToken = null;
        RefreshToken = null;
        UserName = null;
        UserId = 0;
        UserRole = null;
        SecureStorage.Remove("access_token");
        SecureStorage.Remove("refresh_token");
        Preferences.Remove("user_name");
        Preferences.Remove("user_id");
        Preferences.Remove("user_role");
        await Task.CompletedTask;
    }
}
