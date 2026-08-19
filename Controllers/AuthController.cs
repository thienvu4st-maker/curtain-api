using media_app_api.DTOs;
using media_app_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace media_app_api.Controllers;

[EnableCors("AllowFlutter")]
[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var response = await authService.RegisterAsync(request);
        if (response is null)
            return BadRequest(new { message = "Username already exists." });

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var response = await authService.LoginAsync(request);
        if (response is null)
            return Unauthorized(new { message = "Invalid username or password." });

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var response = await authService.RefreshTokenAsync(request);
        if (response is null)
            return Unauthorized(new { message = "Invalid or expired refresh token." });

        return Ok(response);
    }

    [Authorize]
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken()
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
            return BadRequest(new { message = "Invalid user identity." });

        await authService.RevokeTokenAsync(username);
        return Ok(new { message = "Token revoked successfully." });
    }
}
