
using FitnessClub.Api.Dtos.Specializations;
using FitnessClub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления специализациями тренеров.
/// </summary>
[ApiController]
[Route("api/specializations")]
public class SpecializationsController(SpecializationService service, ILogger<SpecializationsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все специализации.
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<SpecializationDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получить специализацию по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<SpecializationDto> GetById(int id)
    {
        var specialization = service.GetById(id);

        if (specialization is null)
        {
            return NotFound();
        }

        return Ok(specialization);
    }

    /// <summary>
    /// Создать специализацию.
    /// </summary>
    [HttpPost]
    public ActionResult<SpecializationDto> Create(CreateSpecializationDto dto)
    {
        try
        {
            var specialization = service.Create(dto);

            logger.LogInformation(
                "Создана специализация с ID {Id}",
                specialization.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = specialization.Id },
                specialization);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Обновить специализацию.
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CreateSpecializationDto dto)
    {
        try
        {
            if (!service.Update(id, dto))
            {
                return NotFound();
            }

            logger.LogInformation(
                "Обновлена специализация с ID {Id}", id);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить специализацию.
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!service.Delete(id))
            {
                return NotFound();
            }

            logger.LogInformation(
                "Удалена специализация с ID {Id}", id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
