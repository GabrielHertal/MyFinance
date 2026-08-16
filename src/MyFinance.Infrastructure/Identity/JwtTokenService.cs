using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Services;

namespace MyFinance.Infrastructure.Identity;

public sealed class JwtTokenService : ITokenService
{
    private readonly JwtSettings _settings;

    public JwtTokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public TokenDto Generate(Guid userId, string email, IEnumerable<string> roles)
    {
        var now = DateTime.UtcNow;
        var accessTokenExpiresAt = now.AddMinutes(_settings.AccessTokenMinutes);
        var refreshTokenExpiresAt = now.AddDays(_settings.RefreshTokenDays);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, userId.ToString())
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(_settings.Issuer,
                                       _settings.Audience,
                                       claims,
                                       now,
                                       accessTokenExpiresAt,
                                       credentials);

        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        return new TokenDto(new JwtSecurityTokenHandler().WriteToken(jwt),
                            accessTokenExpiresAt,
                            refreshToken,
                            refreshTokenExpiresAt);
    }

    public string HashRefreshToken(string refreshToken) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));

    public bool VerifyRefreshToken(string refreshToken, string storedHash)
    {
        var computedHash = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));

        if (storedHash.Length != computedHash.Length * 2)
            return false;

        try
        {
            var storedHashBytes = Convert.FromHexString(storedHash);
            return CryptographicOperations.FixedTimeEquals(computedHash, storedHashBytes);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
