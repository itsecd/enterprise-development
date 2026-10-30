using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FitnessGym.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// Аутентификация пользователей
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Авторизация и получение JWT токена
    /// </summary>
    /// <param name="request">Логин и пароль</param>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username != "admin" || request.Password != "password")
            return Unauthorized("Неверный логин или пароль");

        var jwtConfig = configuration.GetSection("JwtSettings");
        var secret = jwtConfig["SecretKey"] ?? throw new InvalidOperationException("SecretKey не найден");
        var issuer = jwtConfig["Issuer"];
        var audience = jwtConfig["Audience"];

        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.Username) }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return Ok(new LoginResponse(handler.WriteToken(token)));
    }
}