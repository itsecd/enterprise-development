using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Управление выставками музея
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExhibitionsController(
    IExhibitionService service,
    ILogger<ExhibitionsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех выставок
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExhibitionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExhibitionResponse>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка выставок");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить выставку по её идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExhibitionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExhibitionResponse>> GetById(int id)
    {
        try
        {
            var result = await service.GetByIdAsync(id);
            if (result == null)
            {
                logger.LogWarning("Выставка с ID {Id} не найдена", id);
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении выставки с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать новую выставку
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ExhibitionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExhibitionResponse>> Create([FromBody] ExhibitionCreateRequest request)
    {
        try
        {
            var result = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании выставки");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные существующей выставки
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, [FromBody] ExhibitionUpdateRequest request)
    {
        try
        {
            var success = await service.UpdateAsync(id, request);
            if (!success)
            {
                logger.LogWarning("Выставка с ID {Id} не найдена для обновления", id);
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении выставки с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить выставку
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            // Идемпотентный DELETE: если объекта нет, это не ошибка
            await service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении выставки с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}