namespace MyFinance.Application.DTOs;

public sealed record RegisterRequest(string Nome, string Email, string Password);

public sealed record LoginRequest(string Email, string Password);

public sealed record RefreshTokenRequest(Guid UserId, string RefreshToken);
