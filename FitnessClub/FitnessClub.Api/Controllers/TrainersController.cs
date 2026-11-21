using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Предоставляет REST API для управления тренерами фитнес-клуба.
/// </summary>
/// <param name="trainerService">Сервис управления тренерами.</param>
/// <param name="logger">Журнал событий.</param>
[ApiController]
[Route("api/trainers")]
[Produces("application/json")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class TrainersController(
    ITrainerService trainerService,
    ILogger<TrainersController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех тренеров.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<TrainerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TrainerDto>>> GetAllAsync()
    {
        try
        {
            var trainers = await trainerService.GetAllAsync();

            return Ok(trainers);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>
    /// Возвращает тренера по ID.
    /// </summary>
    /// <param name="id">ID тренера.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrainerDto>> GetById(int id)
    {
        try
        {
            var trainer = await trainerService.GetByIdAsync(id);

            if (trainer is null)
            {
                return NotFound();
            }

            return Ok(trainer);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>
    /// Создаёт нового тренера.
    /// </summary>
    /// <param name="dto">Данные нового тренера.</param>
    [HttpPost]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrainerDto>> CreateAsync(
        [FromBody] CreateTrainerDto dto)
    {
        try
        {
            var trainer = await trainerService.CreateAsync(dto);

            logger.LogInformation(
                "Создан тренер с IDом {TrainerId}.",
                trainer.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = trainer.Id },
                trainer);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные нового тренера.");

            return BadRequest(new { message = exception.Message });
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>
    /// Обновляет данные существующего тренера.
    /// </summary>
    /// <param name="id">ID тренера.</param>
    /// <param name="dto">Новые данные тренера.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(
        int id,
        [FromBody] UpdateTrainerDto dto)
    {
        try
        {
            var updated = await trainerService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            logger.LogInformation(
                "Обновлён тренер с ID {TrainerId}.",
                id);

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные тренера с ID {TrainerId}.",
                id);

            return BadRequest(new { message = exception.Message });
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>
    /// Удаляет тренера.
    /// </summary>
    /// <param name="id">ID тренера.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleted = await trainerService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            logger.LogInformation(
                "Удалён тренер с ID {TrainerId}.",
                id);

            return NoContent();
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }
}
