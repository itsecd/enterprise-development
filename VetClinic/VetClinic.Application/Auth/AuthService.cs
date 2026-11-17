using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using VetClinic.Contracts.Dto.Auth;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Application.Auth;

/// <summary>
/// Проверяет пароль по хешу и выдает подписанный JWT
/// </summary>
/// <param name="users">Репозиторий пользователей</param>
/// <param name="options">Параметры JWT</param>
/// <param name="timeProvider">Источник текущего времени</param>
public class AuthService(
    IUserRepository users,
    IOptions<JwtOptions> options,
    TimeProvider timeProvider) : IAuthService
{
    private static readonly PasswordHasher<User> _passwordHasher = new();

    /// <inheritdoc/>
    public async Task<TokenDto?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var user = await users.GetByUsernameAsync(dto.Username, cancellationToken);
        if (user is null
            || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
        {
            return null;
        }

        var jwt = options.Value;
        var expiresAt = timeProvider.GetUtcNow().UtcDateTime.AddMinutes(jwt.LifetimeMinutes);
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = jwt.Issuer,
            Audience = jwt.Audience,
            Expires = expiresAt,
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            ]),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                SecurityAlgorithms.HmacSha256)
        };

        return new TokenDto
        {
            AccessToken = new JsonWebTokenHandler().CreateToken(descriptor),
            ExpiresAt = expiresAt
        };
    }
}
