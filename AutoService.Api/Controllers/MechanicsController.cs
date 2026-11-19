using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Api.Controllers;

/// <summary>
/// Контроллер для управления механиками.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MechanicsController(IMechanicService mechanicService) : ControllerBase
{
    /// <summary>
    /// Получить список всех механиков.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MechanicDto>>> GetAll()
    {
        return Ok(await mechanicService.GetAllAsync());
    }

    /// <summary>
    /// Получить механика по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор механика.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<MechanicDto>> GetById(Guid id)
    {
        var mechanic = await mechanicService.GetByIdAsync(id);
        if (mechanic == null) return NotFound();
        return Ok(mechanic);
    }

    /// <summary>
    /// Добавить нового механика.
    /// </summary>
    /// <param name="newMechanic">Данные нового механика.</param>
    [HttpPost]
    public async Task<ActionResult<MechanicDto>> Create([FromBody] MechanicDto newMechanic)
    {
        var created = await mechanicService.CreateAsync(newMechanic);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить данные существующего механика.
    /// </summary>
    /// <param name="id">Идентификатор механика.</param>
    /// <param name="updatedMechanic">Новые данные механика.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MechanicUpdateDto updatedMechanic)
    {
        await mechanicService.UpdateAsync(id, updatedMechanic);
        return NoContent();
    }

    /// <summary>
    /// Удалить механика по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор механика.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mechanicService.DeleteAsync(id);
        return NoContent();
    }
}