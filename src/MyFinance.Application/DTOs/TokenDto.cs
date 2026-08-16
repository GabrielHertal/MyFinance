namespace MyFinance.Application.DTOs;

public sealed record TokenDto(string AccessToken, DateTime AccessTokenExpiresAtUtc, string RefreshToken, DateTime RefreshTokenExpiresAtUtc);