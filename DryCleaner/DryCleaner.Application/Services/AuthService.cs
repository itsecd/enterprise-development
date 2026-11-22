
using DryCleaner.Application.Contracts.Auth;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Mappers;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для аутентификации пользователей.
/// </summary>
/// <param name="userRepository">Репозиторий для доступа к данным пользователей.</param>
/// <param name="passwordHasher">Сервис для хэширования и проверки паролей.</param>
/// <param name="config">Конфигурация приложения.</param>
public class AuthService(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    IConfiguration config) : IAuthService
{
    /// <summary>
    /// Выполняет аутентификацию пользователя и создаёт JWT-токен.
    /// </summary>
    /// <param name="request">Данные для аутентификации.</param>
    /// <returns>JWT-токен или null, если данные неверны.</returns>
    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var requestedUser = AuthMapper.Map(request);

        var user = await userRepository.GetByUsername(requestedUser.Username);

        if (user is null)
            return null;

        var verificationResult = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
            return null;

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims:
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            ],
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(config["Jwt:ExpireMinutes"]!)),
            signingCredentials: new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256));

        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return AuthMapper.Map(tokenString);
    }
}
