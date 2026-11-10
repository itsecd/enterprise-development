using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает HTTP-запросы для работы с выставками</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class ExhibitionsController(
    IExhibitionService exhibitionService,
    ILogger<ExhibitionsController> logger) : ControllerBase
{
    /// <summary>Возвращает список всех выставок</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ExhibitionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ExhibitionDto>>> GetAll()
    {
        var exhibitions = await exhibitionService.GetAllAsync();
        return Ok(exhibitions);
    }

    /// <summary>Возвращает выставку по её идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ExhibitionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExhibitionDto>> GetById(int id)
    {
        var exhibition = await exhibitionService.GetByIdAsync(id);

        if (exhibition is null)
        {
            logger.LogWarning("Выставка {ExhibitionId} не найдена", id);
            return NotFound();
        }

        return Ok(exhibition);
    }

    /// <summary>Создаёт новую выставку</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ExhibitionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExhibitionDto>> Create(ExhibitionEditDto dto)
    {
        logger.LogInformation("Создание выставки {ExhibitionName}", dto.Name);

        var exhibition = await exhibitionService.CreateAsync(dto);


        return CreatedAtAction(
            nameof(GetById),
            new { id = exhibition.Id },
            exhibition);
    }

    /// <summary>Обновляет сведения о выставке</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ExhibitionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExhibitionDto>> Update(int id, ExhibitionEditDto dto)
    {
        logger.LogInformation("Обновление выставки {ExhibitionId}", id);

        var updated = await exhibitionService.UpdateAsync(id, dto);

        if (!updated)
        {
            logger.LogWarning("Выставка {ExhibitionId} для обновления не найдена", id);
            return NotFound();
        }

        var exhibition = await exhibitionService.GetByIdAsync(id);
        return Ok(exhibition);
    }

    /// <summary>Удаляет выставку, если она не используется в экскурсиях</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("Удаление выставки {ExhibitionId}", id);

        try
        {
            var deleted = await exhibitionService.DeleteAsync(id);

            if (!deleted)
            {
                logger.LogWarning("Выставка {ExhibitionId} для удаления не найдена", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            logger.LogWarning(exception, "Не удалось удалить выставку {ExhibitionId}", id);
            return Conflict(exception.Message);
        }
    }
}
