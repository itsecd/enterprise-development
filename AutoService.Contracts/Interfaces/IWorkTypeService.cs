using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с видами работ.
/// </summary>
public interface IWorkTypeService
{
    /// <summary>
    /// Получить список всех видов работ.
    /// </summary>
    public Task<IEnumerable<WorkTypeDto>> GetAllAsync();

    /// <summary>
    /// Получить вид работ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вида работ.</param>
    public Task<WorkTypeDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новый вид работ.
    /// </summary>
    /// <param name="workDto">Данные нового вида работ.</param>
    public Task<WorkTypeDto> CreateAsync(WorkTypeDto workDto);

    /// <summary>
    /// Обновить данные вида работ.
    /// </summary>
    /// <param name="id">Идентификатор вида работ.</param>
    /// <param name="workDto">Новые данные вида работ.</param>
    public Task UpdateAsync(Guid id, WorkTypeUpdateDto workDto);

    /// <summary>
    /// Удалить вид работ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вида работ.</param>
    public Task DeleteAsync(Guid id);
}
