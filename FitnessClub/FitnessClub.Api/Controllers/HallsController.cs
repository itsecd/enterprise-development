using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с залами.
/// </summary>
/// <param name="logger">Журнал ошибок контроллера.</param>
/// <param name="service">Сервис залов.</param>
[ApiController]
[Route("api/halls")]
[Produces("application/json")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class HallsController(IHallService service, ILogger<HallsController> logger) : ControllerBase
{
    /// <summary>Возвращает все залы.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<HallDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<HallDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>Возвращает зал по ID.</summary>
    /// <param name="id">ID зала.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(HallDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HallDto>> GetById(int id)
    {
        try
        {
            var hall = await service.GetByIdAsync(id);

            return hall is null ? NotFound() : Ok(hall);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>Создаёт зал.</summary>
    /// <param name="dto">Данные нового зала.</param>
    [HttpPost]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(HallDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HallDto>> Create([FromBody] CreateHallDto dto)
    {
        try
        {
            var hall = await service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = hall.Id }, hall);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>Обновляет зал.</summary>
    /// <param name="id">ID зала.</param>
    /// <param name="dto">Новые данные зала.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateHallDto dto)
    {
        try
        {
            var updated = await service.UpdateAsync(id, dto);

            return updated ? NoContent() : NotFound();
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>Удаляет зал.</summary>
    /// <param name="id">ID зала.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteAsync(id);

            return deleted ? NoContent() : NotFound();
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }
}
