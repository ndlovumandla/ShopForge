using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopForge.Api.Services.Interfaces;
using ShopForge.Database;
using ShopForge.Database.Entities;
using ShopForge.Shared.Constants;
using ShopForge.Shared.DTOs.Auth;
using ShopForge.Shared.DTOs.Common;

namespace ShopForge.Api.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly ShopForgeDbContext _db;
    private readonly IConfiguration _config;

    public AuthService(ShopForgeDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        if (await _db.Users.AnyAsync(u => u.Email == request.Email.ToLower()))
            return ApiResponse<AuthResponse>.Fail("Email already registered.");

        var user = new User
        {
            Email = request.Email.ToLower(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = AppConstants.Roles.Customer,
            EmailVerified = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return await BuildAuthResponseAsync(user);
    }

    public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email.ToLower());
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return ApiResponse<AuthResponse>.Fail("Invalid email or password.");

        if (!user.IsActive)
            return ApiResponse<AuthResponse>.Fail("Account is disabled.");

        user.LastLoginAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return await BuildAuthResponseAsync(user);
    }

    public async Task<ApiResponse<AuthResponse>> RefreshTokenAsync(string refreshToken)
    {
        var token = await _db.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsRevoked);

        if (token == null || token.ExpiresAt < DateTime.UtcNow)
            return ApiResponse<AuthResponse>.Fail("Invalid or expired refresh token.");

        token.IsRevoked = true;
        await _db.SaveChangesAsync();

        return await BuildAuthResponseAsync(token.User);
    }

    public async Task<ApiResponse<bool>> LogoutAsync(int userId, string refreshToken)
    {
        var token = await _db.RefreshTokens.FirstOrDefaultAsync(
            rt => rt.UserId == userId && rt.Token == refreshToken);

        if (token != null)
        {
            token.IsRevoked = true;
            await _db.SaveChangesAsync();
        }

        return ApiResponse<bool>.Ok(true, "Logged out successfully.");
    }

    public async Task<ApiResponse<UserProfileDto>> GetProfileAsync(int userId)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return ApiResponse<UserProfileDto>.Fail("User not found.");
        return ApiResponse<UserProfileDto>.Ok(MapToProfileDto(user));
    }

    public async Task<ApiResponse<UserProfileDto>> UpdateProfileAsync(int userId, UpdateProfileRequest request)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return ApiResponse<UserProfileDto>.Fail("User not found.");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return ApiResponse<UserProfileDto>.Ok(MapToProfileDto(user));
    }

    public async Task<ApiResponse<bool>> ChangePasswordAsync(int userId, ChangePasswordRequest request)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return ApiResponse<bool>.Fail("User not found.");

        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
            return ApiResponse<bool>.Fail("Current password is incorrect.");

        if (request.NewPassword != request.ConfirmNewPassword)
            return ApiResponse<bool>.Fail("Passwords do not match.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Password changed successfully.");
    }

    public async Task<ApiResponse<bool>> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        // In a real system, send email. For now, just acknowledge.
        await Task.CompletedTask;
        return ApiResponse<bool>.Ok(true, "If that email exists, a reset link has been sent.");
    }

    public async Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email.ToLower());
        if (user == null) return ApiResponse<bool>.Fail("Invalid reset request.");

        // Token validation would happen here in a real system.
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Password reset successfully.");
    }

    private async Task<ApiResponse<AuthResponse>> BuildAuthResponseAsync(User user)
    {
        var accessToken = GenerateJwtToken(user);
        var refreshToken = await CreateRefreshTokenAsync(user.Id);
        var expiryMinutes = int.Parse(_config["Jwt:ExpiryMinutes"] ?? "60");

        return ApiResponse<AuthResponse>.Ok(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes),
            User = MapToProfileDto(user)
        });
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiryMinutes = int.Parse(_config["Jwt:ExpiryMinutes"] ?? "60");

        var claims = new[]
        {
            new Claim(AppConstants.JwtClaims.UserId, user.Id.ToString()),
            new Claim(AppConstants.JwtClaims.Email, user.Email),
            new Claim(AppConstants.JwtClaims.Role, user.Role),
            new Claim(AppConstants.JwtClaims.FirstName, user.FirstName),
            new Claim(AppConstants.JwtClaims.LastName, user.LastName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> CreateRefreshTokenAsync(int userId)
    {
        var tokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = tokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow
        };
        _db.RefreshTokens.Add(refreshToken);
        await _db.SaveChangesAsync();
        return tokenValue;
    }

    private static UserProfileDto MapToProfileDto(User user) => new()
    {
        Id = user.Id,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        PhoneNumber = user.PhoneNumber,
        Role = user.Role,
        ProfileImageUrl = user.ProfileImageUrl,
        EmailVerified = user.EmailVerified,
        CreatedAt = user.CreatedAt
    };
}
