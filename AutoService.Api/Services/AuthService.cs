using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AutoService.Api.Services;

/// <summary>
/// Сервис проверки учетных данных и создания JWT
/// </summary>
public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
{
    private static readonly PasswordHasher<User> _passwordHasher = new();

    /// <summary>
    /// Проверить учетные данные пользователя и создать JWT
    /// </summary>
    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto)
    {
        var user = await userRepository.GetByUsernameAsync(loginDto.Username);
        if (user is null) return null;

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        if (verificationResult == PasswordVerificationResult.Failed) return null;

        var expiresAt = DateTime.UtcNow.AddHours(1);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetRequiredSetting("Key")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: GetRequiredSetting("Issuer"),
            audience: GetRequiredSetting("Audience"),
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expiresAt
        };
    }

    private string GetRequiredSetting(string name) =>
        configuration[$"Jwt:{name}"] ?? throw new InvalidOperationException($"Не настроен параметр Jwt:{name}");
}
