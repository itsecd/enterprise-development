using DryCleaner.Application.Contracts.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер аутентификации.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController(IConfiguration config) : ControllerBase
{
    /// <summary>
    /// Вход по логину и паролю
    /// </summary>
    /// <param name="request">Данные для аутентификации</param>
    /// <returns>JWT-токен.</returns>
    [HttpPost("login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        if (request.Username != config["Auth:Username"] ||
            request.Password != config["Auth:Password"])
        {
            return Unauthorized();
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: [new Claim(ClaimTypes.Name, request.Username)],
            expires: DateTime.UtcNow.AddMinutes(int.Parse(config["Jwt:ExpireMinutes"]!)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new LoginResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) };
    }
}