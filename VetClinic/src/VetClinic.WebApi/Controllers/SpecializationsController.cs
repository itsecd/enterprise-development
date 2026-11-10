using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Dto;
using VetClinic.Application.Mapping;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.WebApi.Controllers;

/// <summary>
/// Управление справочником специализаций врачей
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SpecializationsController(
    IRepository<Specialization> repository,
    ILogger<SpecializationsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех специализаций
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAll()
    {
        try
        {
            var specializations = await repository.GetAllAsync();
            return Ok(specializations.Select(s => s.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка специализаций");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> GetById(int id)
    {
        try
        {
            var specialization = await repository.GetByIdAsync(id);
            if (specialization is null)
            {
                return NotFound();
            }

            return Ok(specialization.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении специализации с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Добавить новую специализацию
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Create([FromBody] CreateSpecializationDto dto)
    {
        try
        {
            var specialization = new Specialization
            {
                Name = dto.Name,
                TargetSpecies = dto.TargetSpecies!.Value
            };

            await repository.AddAsync(specialization);

            return CreatedAtAction(nameof(GetById), new { id = specialization.Id }, specialization.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании специализации");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить специализацию
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateSpecializationDto dto)
    {
        try
        {
            var specialization = await repository.GetByIdAsync(id);
            if (specialization is null)
            {
                return NotFound();
            }

            specialization.Name = dto.Name;
            specialization.TargetSpecies = dto.TargetSpecies!.Value;
            await repository.UpdateAsync(specialization);

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении специализации с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении специализации с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}