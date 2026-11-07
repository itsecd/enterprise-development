using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление сущностями
/// </summary>
/// <typeparam name="TDto">
/// Тип DTO для чтения
/// </typeparam>
/// <typeparam name="TEditDto">
/// Тип DTO для создания и изменения
/// </typeparam>
/// <param name="service">
/// Сервис управления сущностями
/// </param>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TEditDto>
    (ICrudService<TDto, TEditDto> service) : ControllerBase 
    where TDto : IHasId
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <returns>
    /// Список сущностей
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll() =>
        Ok(await service.GetAllAsync());

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">
    /// Идентификатор сущности
    /// </param>
    /// <returns>
    /// Найденная сущность или ответ 404
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TDto>> GetById(int id)
    {
        var entity = await service.GetByIdAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать сущность
    /// </summary>
    /// <param name="dto">
    /// Данные для создания сущности
    /// </param>
    /// <returns>
    /// Созданная сущность
    /// </returns>
    [HttpPost]
    [EndpointSummary("Создать сущность")]
    public async Task<ActionResult<TDto>> Create(TEditDto dto)
    {
        var created = await service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить сущность
    /// </summary>
    /// <param name="id">
    /// Идентификатор сущности
    /// </param>
    /// <param name="dto">
    /// Новые данные сущности
    /// </param>
    /// <returns>
    /// Обновлённая сущность или ответ 404
    /// </returns>
    [HttpPut("{id:int}")]
    [EndpointSummary("Изменить сущность")]
    public async Task<ActionResult<TDto>> Update(int id, TEditDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удалить сущность
    /// </summary>
    /// <param name="id">
    /// Идентификатор сущности
    /// </param>
    /// <returns>
    /// Результат удаления сущности
    /// </returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}