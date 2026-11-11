using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Предоставляет REST API для управления специализациями тренеров.
/// </summary>
[ApiController]
[Route("api/specializations")]
[Produces("application/json")]
/// <param name="specializationService">Сервис специализаций.</param>
/// <param name="logger">Журнал событий.</param>
public class SpecializationsController(
    ISpecializationService specializationService,
    ILogger<SpecializationsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех специализаций.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<SpecializationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<SpecializationDto>>> GetAllAsync()
    {
        var specializations = await specializationService.GetAllAsync();
        return Ok(specializations);
    }

    /// <summary>
    /// Возвращает специализацию по ID.
    /// </summary>
    /// <param name="id">ID специализации.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecializationDto>> GetByIdAsync(int id)
    {
        var specialization = await specializationService.GetByIdAsync(id);

        if (specialization is null)
        {
            return NotFound();
        }

        return Ok(specialization);
    }

    /// <summary>
    /// Создаёт новую специализацию.
    /// </summary>
    /// <param name="dto">Данные новой специализации.</param>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SpecializationDto>> CreateAsync(
        [FromBody] CreateSpecializationDto dto)
    {
        try
        {
            var specialization = await specializationService.CreateAsync(dto);

            logger.LogInformation(
                "Создана специализация с ID {SpecializationId}.",
                specialization.Id);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = specialization.Id },
                specialization);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные специализации.");
            return BadRequest(new { message = exception.Message });
        }
    }

    /// <summary>
    /// Обновляет существующую специализацию.
    /// </summary>
    /// <param name="id">ID специализации.</param>
    /// <param name="dto">Новые данные специализации.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(
        int id,
        [FromBody] UpdateSpecializationDto dto)
    {
        try
        {
            var updated = await specializationService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            logger.LogInformation(
                "Обновлена специализация с ID {SpecializationId}.",
                id);

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные специализации {SpecializationId}.",
                id);

            return BadRequest(new { message = exception.Message });
        }
    }

    /// <summary>
    /// Удаляет специализацию.
    /// </summary>
    /// <param name="id">ID специализации.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var deleted = await specializationService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        logger.LogInformation(
            "Удалена специализация с ID {SpecializationId}.",
            id);

        return NoContent();
    }
}