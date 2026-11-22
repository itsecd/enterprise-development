
using DryCleaner.Application.Contracts.Auth;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер аутентификации.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController(
    IAuthService authService,
    ILogger<AuthController> logger) : ControllerBase
{
    /// <summary>
    /// Выполняет вход пользователя в систему.
    /// </summary>
    /// <param name="request">Данные для аутентификации.</param>
    /// <returns>JWT-токен или Unauthorized, если данные неверны.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        try
        {
            var result = await authService.LoginAsync(request);

            return result == null ? Unauthorized() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError("{method} method of {Pcontroller} was called with parameter: Username={username}. Ended with an error: {errorText}",
                nameof(Login), GetType().Name, request.Username, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }
    }
}
