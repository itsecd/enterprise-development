using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Управление билетами
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController(
    ITicketService service,
    ILogger<TicketsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех билетов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TicketResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketResponse>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка билетов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить билет по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketResponse>> GetById(int id)
    {
        try
        {
            var result = await service.GetByIdAsync(id);
            if (result == null)
            {
                logger.LogWarning("Билет с ID {Id} не найден", id);
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении билета с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать новый билет
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TicketResponse>> Create([FromBody] TicketCreateRequest request)
    {
        try
        {
            var result = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при создании билета");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании билета");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные билета
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, [FromBody] TicketUpdateRequest request)
    {
        try
        {
            var success = await service.UpdateAsync(id, request);
            if (!success)
            {
                logger.LogWarning("Билет с ID {Id} не найден для обновления", id);
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении билета с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить билет
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
            logger.LogError(ex, "Ошибка при удалении билета с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}