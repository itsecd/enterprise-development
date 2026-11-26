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
/// <param name="service">Сервис управления сущностями</param>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TEditDto>
    (ICrudService<TDto, TEditDto> service) : ControllerBase 
    where TDto : IHasId
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список сущностей</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await service.GetAllAsync(cancellationToken));

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Найденная сущность или ответ 404</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TDto>> GetById(int id, CancellationToken cancellationToken)
    {
        return Ok(await service.GetByIdAsync(id, cancellationToken));
    }

    /// <summary>
    /// Создать сущность
    /// </summary>
    /// <param name="dto">Данные для создания сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Созданная сущность</returns>
    [HttpPost]
    [EndpointSummary("Создать сущность")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TDto>> Create(TEditDto dto, CancellationToken cancellationToken)
    {
        var created = await service.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить сущность
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="dto">Новые данные сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Обновлённая сущность или ответ 404</returns>
    [HttpPut("{id:int}")]
    [EndpointSummary("Изменить сущность")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TDto>> Update(int id, TEditDto dto, CancellationToken cancellationToken)
    {
        return Ok(await service.UpdateAsync(id, dto, cancellationToken));
    }

    /// <summary>
    /// Удалить сущность
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Результат удаления сущности</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}