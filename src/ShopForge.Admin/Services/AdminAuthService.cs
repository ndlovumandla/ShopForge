namespace ShopForge.Admin.Services;

public class AdminAuthService : IAdminAuthService
{
    private readonly IHttpContextAccessor _httpContext;
    private string? _token;
    private string? _refreshToken;
    private int? _userId;
    private string? _userName;
    private string? _role;

    public AdminAuthService(IHttpContextAccessor httpContext) => _httpContext = httpContext;

    public bool IsAuthenticated => !string.IsNullOrEmpty(Token);
    public string? Token => _token ?? _httpContext.HttpContext?.Request.Cookies["admin_token"];
    public string? RefreshToken => _refreshToken ?? _httpContext.HttpContext?.Request.Cookies["admin_refresh_token"];
    public int UserId
    {
        get
        {
            if (_userId.HasValue)
            {
                return _userId.Value;
            }

            var cookieValue = _httpContext.HttpContext?.Request.Cookies["admin_user_id"];
            return int.TryParse(cookieValue, out var parsed) ? parsed : 0;
        }
    }
    public string? UserName => _userName ?? _httpContext.HttpContext?.Request.Cookies["admin_user"];
    public string? Role => _role ?? _httpContext.HttpContext?.Request.Cookies["admin_role"];

    public void SetAuth(string token, string refreshToken, int userId, string userName, string role)
    {
        _token = token;
        _refreshToken = refreshToken;
        _userId = userId;
        _userName = userName;
        _role = role;
    }

    public void ClearAuth()
    {
        _token = null;
        _refreshToken = null;
        _userId = null;
        _userName = null;
        _role = null;
    }
}
