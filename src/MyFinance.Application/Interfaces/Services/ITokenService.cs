using MyFinance.Application.DTOs;

namespace MyFinance.Application.Interfaces.Services;

public interface ITokenService
{
    TokenDto Generate(Guid userId, string email, IEnumerable<string> roles);
    string HashRefreshToken(string refreshToken);
    bool VerifyRefreshToken(string refreshToken, string storedHash);
}