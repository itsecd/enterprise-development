using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает HTTP-запросы для работы с выставками</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class ExhibitionsController(IExhibitionService exhibitionService) : ControllerBase
{
    /// <summary>Возвращает список всех выставок</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExhibitionDto>>> GetAll()
    {
        var exhibitions = await exhibitionService.GetAllAsync();
        return Ok(exhibitions);
    }

    /// <summary>Возвращает выставку по её идентификатору</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExhibitionDto>> GetById(int id)
    {
        var exhibition = await exhibitionService.GetByIdAsync(id);

        if (exhibition is null)
            return NotFound();

        return Ok(exhibition);
    }

    /// <summary>Создаёт новую выставку</summary>
    [HttpPost]
    public async Task<ActionResult<ExhibitionDto>> Create(ExhibitionEditDto dto)
    {
        var exhibition = await exhibitionService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = exhibition.Id },
            exhibition);
    }

    /// <summary>Обновляет сведения о выставке</summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ExhibitionDto>> Update(int id, ExhibitionEditDto dto)
    {
        var updated = await exhibitionService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        var exhibition = await exhibitionService.GetByIdAsync(id);
        return Ok(exhibition);
    }

    /// <summary>Удаляет выставку, если она не используется в экскурсиях</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await exhibitionService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }
}
