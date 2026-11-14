using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с тренировками.
/// </summary>
[ApiController]
[Route("api/trainings")]
[Produces("application/json")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class TrainingsController(
    ITrainingService service,
    ILogger<TrainingsController> logger) : ControllerBase
{
    /// <summary>Возвращает все записи на тренировки.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<TrainingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TrainingDto>>> GetAll()
    {
        return Ok(await service.GetAllAsync());
    }

    /// <summary>Возвращает запись по IDу.</summary>
    /// <param name="id">ID тренировки.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrainingDto>> GetById(int id)
    {
        var training = await service.GetByIdAsync(id);

        return training is null ? NotFound() : Ok(training);
    }

    /// <summary>Создаёт запись на тренировку.</summary>
    /// <param name="dto">Данные тренировки.</param>
    [HttpPost]
    [ProducesResponseType(typeof(TrainingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrainingDto>> Create(
        [FromBody] CreateTrainingDto dto)
    {
        try
        {
            var training = await service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = training.Id },
                training);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные тренировки.");

            return BadRequest(new { message = exception.Message });
        }
    }

    /// <summary>Обновляет запись на тренировку.</summary>
    /// <param name="id">ID тренировки.</param>
    /// <param name="dto">Новые данные тренировки.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateTrainingDto dto)
    {
        try
        {
            var updated = await service.UpdateAsync(id, dto);

            return updated ? NoContent() : NotFound();
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные тренировки {TrainingId}.",
                id);

            return BadRequest(new { message = exception.Message });
        }
    }

    /// <summary>Удаляет запись на тренировку.</summary>
    /// <param name="id">ID тренировки.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);

        return deleted ? NoContent() : NotFound();
    }
}