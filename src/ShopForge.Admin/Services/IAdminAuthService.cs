namespace ShopForge.Admin.Services;

public interface IAdminAuthService
{
    bool IsAuthenticated { get; }
    string? Token { get; }
    string? RefreshToken { get; }
    int UserId { get; }
    string? UserName { get; }
    string? Role { get; }
    void SetAuth(string token, string refreshToken, int userId, string userName, string role);
    void ClearAuth();
}
