using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Dto;
using VetClinic.Application.Mapping;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.WebApi.Controllers;

/// <summary>
/// Управление справочником пород
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BreedsController(
    IRepository<Breed> repository,
    ILogger<BreedsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все породы
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BreedDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BreedDto>>> GetAll()
    {
        try
        {
            var breeds = await repository.GetAllAsync();
            return Ok(breeds.Select(b => b.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка пород");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить породу по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BreedDto>> GetById(int id)
    {
        try
        {
            var breed = await repository.GetByIdAsync(id);
            if (breed is null)
            {
                return NotFound();
            }

            return Ok(breed.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении породы с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Добавить новую породу
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BreedDto>> Create([FromBody] CreateBreedDto dto)
    {
        try
        {
            var breed = new Breed
            {
                Name = dto.Name,
                Species = dto.Species!.Value
            };

            await repository.AddAsync(breed);

            return CreatedAtAction(nameof(GetById), new { id = breed.Id }, breed.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при добавлении породы");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить существующую породу
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateBreedDto dto)
    {
        try
        {
            var breed = await repository.GetByIdAsync(id);
            if (breed is null)
            {
                return NotFound();
            }

            breed.Name = dto.Name;
            breed.Species = dto.Species!.Value;
            await repository.UpdateAsync(breed);

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении породы с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить породу
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
            logger.LogError(ex, "Ошибка при удалении породы с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}