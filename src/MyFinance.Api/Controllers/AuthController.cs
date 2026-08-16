using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Services;
using MyFinance.Infrastructure.Identity;

namespace MyFinance.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private const string LoginProvider = "MyFinance";
    private const string RefreshTokenName = "RefreshTokenHash";
    private const string RefreshTokenExpiryName = "RefreshTokenExpiresAtUtc";
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new ApplicationUser { Nome = request.Nome, Email = request.Email, UserName = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors.Select(x => new { x.Code, x.Description }) });
        var token = await CreateAndStoreTokensAsync(user);
        return Created(string.Empty, new { user.Id, token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized(new { code = StatusCodes.Status401Unauthorized, message = "E-mail ou senha inválidos." });

        return Ok(await CreateAndStoreTokensAsync(user));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Unauthorized(new { code = StatusCodes.Status401Unauthorized, message = "Refresh token inválido." });

        var storedHash = await _userManager.GetAuthenticationTokenAsync(user, LoginProvider, RefreshTokenName);
        var storedExpiry = await _userManager.GetAuthenticationTokenAsync(user, LoginProvider, RefreshTokenExpiryName);
        var validExpiry = DateTime.TryParse(storedExpiry, CultureInfo.InvariantCulture,
            DateTimeStyles.RoundtripKind, out var expiresAt);

        if (storedHash is null || !_tokenService.VerifyRefreshToken(request.RefreshToken, storedHash) ||
            !validExpiry || expiresAt <= DateTime.UtcNow)
            return Unauthorized(new { code = StatusCodes.Status401Unauthorized , message = "Refresh token inválido ou expirado." });

        return Ok(await CreateAndStoreTokensAsync(user));
    }

    [HttpPost("revoke/{userId:guid}")]
    public async Task<IActionResult> Revoke(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is not null)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, LoginProvider, RefreshTokenName);
            await _userManager.RemoveAuthenticationTokenAsync(user, LoginProvider, RefreshTokenExpiryName);
        }
        return NoContent();
    }

    private async Task<TokenDto> CreateAndStoreTokensAsync(ApplicationUser user)
    {
        try
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.Generate(user.Id, user.Email!, roles);
            await _userManager.SetAuthenticationTokenAsync(user, LoginProvider, RefreshTokenName, _tokenService.HashRefreshToken(token.RefreshToken));
            await _userManager.SetAuthenticationTokenAsync(user, LoginProvider, RefreshTokenExpiryName, token.RefreshTokenExpiresAtUtc.ToString("O", CultureInfo.InvariantCulture));
            return token;
        }
        catch
        {
            throw;
        }
    }
}