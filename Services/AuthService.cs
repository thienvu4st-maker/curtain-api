using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using media_app_api.Data;
using media_app_api.DTOs;
using media_app_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace media_app_api.Services;

public class AuthService(AppDbContext db, IConfiguration configuration) : IAuthService
{
    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto request)
    {
        if (await db.Users.AnyAsync(u => u.Username.ToLower() == request.Username.ToLower()))
            return null; // Username already exists

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        var accessToken = CreateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return new AuthResponseDto(accessToken, refreshToken, user.Username, user.Email);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto request)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == request.Username.ToLower());
        if (user is null)
            return null;

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return null;

        var accessToken = CreateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await db.SaveChangesAsync();

        return new AuthResponseDto(accessToken, refreshToken, user.Username, user.Email);
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal is null)
            return null;

        var username = principal.Identity?.Name;
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return null;

        var newAccessToken = CreateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await db.SaveChangesAsync();

        return new AuthResponseDto(newAccessToken, newRefreshToken, user.Username, user.Email);
    }

    public async Task<bool> RevokeTokenAsync(string username)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user is null)
            return false;

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await db.SaveChangesAsync();

        return true;
    }

    // --- Private Helper Methods ---

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private string CreateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var keySecret = configuration["Jwt:Secret"] ?? "SuperSecretKeyForEnterpriseMediaApp2026!MustBeAtLeast64BytesLongForHmacSha512AlgorithmValidation!";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7), // 7-day Access Token for mobile/desktop admin app
            SigningCredentials = creds,
            Issuer = configuration["Jwt:Issuer"] ?? "MediaAppApi",
            Audience = configuration["Jwt:Audience"] ?? "MediaAppClient"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var keySecret = configuration["Jwt:Secret"] ?? "SuperSecretKeyForEnterpriseMediaApp2026!MustBeAtLeast64BytesLongForHmacSha512AlgorithmValidation!";
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret)),
            ValidateLifetime = false // Ignore expiration time when validating expired access token for refresh
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
        {
            return null;
        }

        return principal;
    }
}
