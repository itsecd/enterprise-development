using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление сущностями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TEditDto>
    (ICrudService<TDto, TEditDto> service) : ControllerBase 
    where TDto : IHasId
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll() =>
        Ok(await service.GetAllAsync());
    /// <summary>
    /// Получить сущность по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TDto>> GetById(int id)
    {
        var entity = await service.GetByIdAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }
    /// <summary>
    /// Создать сущность
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TDto>> Create(TEditDto dto)
    {
        var created = await service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    /// <summary>
    /// Изменить сущность
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<TDto>> Update(int id, TEditDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }
    /// <summary>
    /// Удалить сущность
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}