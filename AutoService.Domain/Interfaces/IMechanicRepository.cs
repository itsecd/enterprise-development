using AutoService.Domain.Models;

namespace AutoService.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с механиками.
/// </summary>
public interface IMechanicRepository
{
    /// <summary>
    /// Получить список всех механиков.
    /// </summary>
    public Task<IEnumerable<Mechanic>> GetAllAsync();

    /// <summary>
    /// Получить механика по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор механика.</param>
    public Task<Mechanic?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить нового механика.
    /// </summary>
    /// <param name="mechanic">Объект механика.</param>
    public Task AddAsync(Mechanic mechanic);

    /// <summary>
    /// Обновить данные существующего механика.
    /// </summary>
    /// <param name="mechanic">Объект механика с обновленными данными.</param>
    public Task UpdateAsync(Mechanic mechanic);

    /// <summary>
    /// Удалить механика.
    /// </summary>
    /// <param name="mechanic">Объект механика для удаления.</param>
    public Task DeleteAsync(Mechanic mechanic);
}