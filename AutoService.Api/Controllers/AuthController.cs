using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Api.Controllers;

/// <summary>
/// Контроллер аутентификации пользователей
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Войти по имени пользователя и паролю
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginDto)
    {
        var response = await authService.LoginAsync(loginDto);
        if (response is null) return Unauthorized();

        return Ok(response);
    }
}
