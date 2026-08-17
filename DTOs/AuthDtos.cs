namespace media_app_api.DTOs;

public record RegisterDto(
    string Username,
    string Email,
    string Password
);

public record LoginDto(
    string Username,
    string Password
);

public record RefreshTokenRequestDto(
    string AccessToken,
    string RefreshToken
);

public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    string Username,
    string Email
);
