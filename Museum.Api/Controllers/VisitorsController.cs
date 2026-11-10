using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает HTTP-запросы для работы с посетителями музея</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class VisitorsController(
    IVisitorService visitorService,
    ILogger<VisitorsController> logger) : ControllerBase
{
    /// <summary>Возвращает список всех посетителей</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<VisitorDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VisitorDto>>> GetAll()
    {
        var visitors = await visitorService.GetAllAsync();
        return Ok(visitors);
    }

    /// <summary>Возвращает посетителя по его идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VisitorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VisitorDto>> GetById(int id)
    {
        var visitor = await visitorService.GetByIdAsync(id);

        if (visitor is null)
        {
            logger.LogWarning("Посетитель {VisitorId} не найден", id);
            return NotFound();
        }

        return Ok(visitor);
    }

    /// <summary>Добавляет нового посетителя</summary>
    [HttpPost]
    [ProducesResponseType(typeof(VisitorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VisitorDto>> Create(VisitorEditDto dto)
    {
        logger.LogInformation("Добавление посетителя {FullName}", dto.FullName);

        var visitor = await visitorService.CreateAsync(dto);


        return CreatedAtAction(
            nameof(GetById),
            new { id = visitor.Id },
            visitor);
    }

    /// <summary>Обновляет сведения о посетителе</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, VisitorEditDto dto)
    {
        logger.LogInformation("Обновление посетителя {VisitorId}", id);

        var updated = await visitorService.UpdateAsync(id, dto);

        if (!updated)
        {
            logger.LogWarning("Посетитель {VisitorId} для обновления не найден", id);
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>Удаляет посетителя, если у него нет билетов</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("Удаление посетителя {VisitorId}", id);

        try
        {
            var deleted = await visitorService.DeleteAsync(id);

            if (!deleted)
            {
                logger.LogWarning("Посетитель {VisitorId} для удаления не найден", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            logger.LogWarning(exception, "Не удалось удалить посетителя {VisitorId}", id);
            return Conflict(exception.Message);
        }
    }
}
