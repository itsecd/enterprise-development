using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Управление экскурсиями
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExcursionsController(
    IExcursionService service,
    ILogger<ExcursionsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех экскурсий
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExcursionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExcursionResponse>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка экскурсий");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить экскурсию по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExcursionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExcursionResponse>> GetById(int id)
    {
        try
        {
            var result = await service.GetByIdAsync(id);
            if (result == null)
            {
                logger.LogWarning("Экскурсия с ID {Id} не найдена", id);
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении экскурсии с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать новую экскурсию
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ExcursionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExcursionResponse>> Create([FromBody] ExcursionCreateRequest request)
    {
        try
        {
            var result = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании экскурсии");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить экскурсию
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, [FromBody] ExcursionUpdateRequest request)
    {
        try
        {
            var success = await service.UpdateAsync(id, request);
            if (!success)
            {
                logger.LogWarning("Экскурсия с ID {Id} не найдена для обновления", id);
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении экскурсии с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить экскурсию
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении экскурсии с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}