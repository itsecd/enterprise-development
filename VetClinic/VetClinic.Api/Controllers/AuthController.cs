using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Dto.Auth;
using VetClinic.Contracts.Services;

namespace VetClinic.Api.Controllers;

/// <summary>
/// Вход и получение токена доступа
/// </summary>
/// <param name="service">Сервис аутентификации</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/auth")]
[Tags("Авторизация")]
[Produces("application/json")]
public class AuthController(IAuthService service, ILogger<AuthController> logger) : ControllerBase
{
    /// <summary>
    /// Получить JWT по имени и паролю
    /// </summary>
    /// <param name="dto">Имя и пароль</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Токен выдан</response>
    /// <response code="400">Данные не прошли валидацию</response>
    /// <response code="401">Неверное имя или пароль</response>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
    {
        var token = await service.LoginAsync(dto, cancellationToken);
        if (token is null)
        {
            logger.LogWarning("Неудачная попытка входа пользователя {Username}", dto.Username);
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Неверное имя или пароль");
        }

        logger.LogInformation("Пользователь {Username} получил токен", dto.Username);
        return Ok(token);
    }
}
