using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Управление посетителями
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VisitorsController(
    IVisitorService service,
    ILogger<VisitorsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех посетителей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VisitorResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VisitorResponse>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка посетителей");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить посетителя по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VisitorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VisitorResponse>> GetById(int id)
    {
        try
        {
            var result = await service.GetByIdAsync(id);
            if (result == null)
            {
                logger.LogWarning("Посетитель с ID {Id} не найден", id);
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении посетителя с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать нового посетителя
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(VisitorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VisitorResponse>> Create([FromBody] VisitorCreateRequest request)
    {
        try
        {
            var result = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании посетителя");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные посетителя
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, [FromBody] VisitorUpdateRequest request)
    {
        try
        {
            var success = await service.UpdateAsync(id, request);
            if (!success)
            {
                logger.LogWarning("Посетитель с ID {Id} не найден для обновления", id);
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении посетителя с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить посетителя
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
            logger.LogError(ex, "Ошибка при удалении посетителя с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}