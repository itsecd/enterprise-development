using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Api.Controllers;

/// <summary>
/// Контроллер для управления видами работ.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WorksController(IWorkTypeService workService) : ControllerBase
{
    /// <summary>
    /// Получить список всех видов работ.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkTypeDto>>> GetAll()
    {
        return Ok(await workService.GetAllAsync());
    }

    /// <summary>
    /// Получить вид работ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вида работ.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkTypeDto>> GetById(Guid id)
    {
        var work = await workService.GetByIdAsync(id);
        if (work == null) return NotFound();
        return Ok(work);
    }

    /// <summary>
    /// Добавить новый вид работ.
    /// </summary>
    /// <param name="newWork">Данные нового вида работ.</param>
    [HttpPost]
    public async Task<ActionResult<WorkTypeDto>> Create([FromBody] WorkTypeDto newWork)
    {
        var created = await workService.CreateAsync(newWork);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить данные вида работ.
    /// </summary>
    /// <param name="id">Идентификатор вида работ.</param>
    /// <param name="updatedWork">Новые данные вида работ.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] WorkTypeUpdateDto updatedWork)
    {
        await workService.UpdateAsync(id, updatedWork);
        return NoContent();
    }

    /// <summary>
    /// Удалить вид работ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вида работ.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await workService.DeleteAsync(id);
        return NoContent();
    }
}