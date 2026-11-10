using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Dto;
using VetClinic.Application.Mapping;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.WebApi.Controllers;

/// <summary>
/// Управление владельцами животных
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OwnersController(
    IRepository<Owner> repository,
    IVetClinicService clinicService,
    ILogger<OwnersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех владельцев
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OwnerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetAll()
    {
        try
        {
            var owners = await repository.GetAllAsync();
            return Ok(owners.Select(o => o.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка владельцев");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить владельцев с более чем одним питомцем
    /// </summary>
    [HttpGet("multiple-pets")]
    [ProducesResponseType(typeof(IEnumerable<OwnerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetOwnersWithMultiplePets()
    {
        try
        {
            var owners = await clinicService.GetOwnersWithMultiplePetsOrderedByNameAsync();
            return Ok(owners.Select(o => o.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка владельцев с несколькими питомцами");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить владельца по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OwnerDto>> GetById(int id)
    {
        try
        {
            var owner = await repository.GetByIdAsync(id);
            if (owner is null)
            {
                return NotFound();
            }

            return Ok(owner.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении владельца с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Добавить владельца
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OwnerDto>> Create([FromBody] CreateOwnerDto dto)
    {
        try
        {
            var owner = new Owner
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await repository.AddAsync(owner);

            return CreatedAtAction(nameof(GetById), new { id = owner.Id }, owner.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при добавлении владельца");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные владельца
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateOwnerDto dto)
    {
        try
        {
            var owner = await repository.GetByIdAsync(id);
            if (owner is null)
            {
                return NotFound();
            }

            owner.FullName = dto.FullName;
            owner.Phone = dto.Phone;
            owner.Address = dto.Address;

            await repository.UpdateAsync(owner);

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении владельца с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить владельца
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
            logger.LogError(ex, "Ошибка при удалении владельца с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}