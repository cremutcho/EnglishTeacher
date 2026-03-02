namespace EnglishTeacher.Application.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; init; } = string.Empty;

    // Data de expiração real do token
    public DateTime Expiration { get; init; }

    public string Email { get; init; } = string.Empty;

    public string Role { get; init; } = string.Empty;
}