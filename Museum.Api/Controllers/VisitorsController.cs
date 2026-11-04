using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает HTTP-запросы для работы с посетителями музея</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class VisitorsController(IVisitorService visitorService) : ControllerBase
{
    /// <summary>Возвращает список всех посетителей</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VisitorDto>>> GetAll()
    {
        var visitors = await visitorService.GetAllAsync();
        return Ok(visitors);
    }

    /// <summary>Возвращает посетителя по его идентификатору</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<VisitorDto>> GetById(int id)
    {
        var visitor = await visitorService.GetByIdAsync(id);

        if (visitor is null)
            return NotFound();

        return Ok(visitor);
    }

    /// <summary>Добавляет нового посетителя</summary>
    [HttpPost]
    public async Task<ActionResult<VisitorDto>> Create(VisitorEditDto dto)
    {
        var visitor = await visitorService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = visitor.Id },
            visitor);
    }

    /// <summary>Обновляет сведения о посетителе</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, VisitorEditDto dto)
    {
        var updated = await visitorService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    /// <summary>Удаляет посетителя, если у него нет билетов</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await visitorService.DeleteAsync(id);

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
