using AutoService.Domain.Models;

namespace AutoService.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с видами работ.
/// </summary>
public interface IWorkTypeRepository
{
    /// <summary>
    /// Получить список всех видов работ.
    /// </summary>
    public Task<IEnumerable<WorkType>> GetAllAsync();

    /// <summary>
    /// Получить вид работ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вида работ.</param>
    public Task<WorkType?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новый вид работ.
    /// </summary>
    /// <param name="workType">Объект вида работ.</param>
    public Task AddAsync(WorkType workType);

    /// <summary>
    /// Обновить данные существующего вида работ.
    /// </summary>
    /// <param name="workType">Объект вида работ с обновленными данными.</param>
    public Task UpdateAsync(WorkType workType);

    /// <summary>
    /// Удалить вид работ.
    /// </summary>
    /// <param name="workType">Объект вида работ для удаления.</param>
    public Task DeleteAsync(WorkType workType);
}