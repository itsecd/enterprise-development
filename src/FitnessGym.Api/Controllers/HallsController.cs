using System.ComponentModel.DataAnnotations;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// Получение списка залов и проверка их доступности.
/// </summary>
/// <param name="service">Сервис залов.</param>
[ApiController]
[Route("api/halls")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class HallsController(IHallService service) : ControllerBase
{
    /// <summary>
    /// Возвращает названия залов, встречающихся в записях на занятия.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<string>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<string>>> GetHalls() =>
        Ok(await service.GetHallsAsync());

    /// <summary>
    /// Проверяет доступность зала; занятия без времени окончания не учитываются.
    /// </summary>
    [HttpGet("{hallName}/availability")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> IsHallAvailable(
        [Required] string hallName, [FromQuery] DateTime? at) =>
        Ok(await service.IsHallAvailableAsync(hallName, at));
}
