using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MuseumSystem.Api.Auth;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Аутентификация пользователей
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IConfiguration configuration,
    ILogger<AuthController> logger,
    HashTableUserStore userStore) : ControllerBase
{
    /// <summary>
    /// Авторизация и получение JWT токена
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            // Проверка через хэш-таблицу
            if (!userStore.Validate(request.Username, request.Password))
            {
                logger.LogWarning("Неудачная попытка входа для пользователя {Username}", request.Username);
                return Unauthorized("Неверный логин или пароль");
            }

            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("SecretKey не найден");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.Username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            logger.LogInformation("Пользователь {Username} успешно авторизован", request.Username);
            return Ok(new LoginResponse { Token = tokenHandler.WriteToken(token) });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при авторизации пользователя {Username}", request.Username);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        try
        {
            // Валидация (базовая защита)
            if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Length < 3)
                return BadRequest("Логин должен содержать минимум 3 символа.");

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
                return BadRequest("Пароль должен содержать минимум 6 символов.");

            // Попытка регистрации (защита от дубликатов)
            if (!userStore.TryRegister(request.Username, request.Password))
            {
                logger.LogWarning("Попытка регистрации с существующим логином: {Username}", request.Username);
                return Conflict("Пользователь с таким логином уже существует.");
            }

            logger.LogInformation("Успешная регистрация нового пользователя: {Username}", request.Username);
            return Ok(new { Message = "Пользователь успешно зарегистрирован." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при регистрации пользователя {Username}", request.Username);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}