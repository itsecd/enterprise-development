using DryCleaner.Domain.Entities;

namespace DryCleaner.Domain.Repositories;

/// <summary>
/// Базовый интерфейс для работы с сущностями
/// </summary>
public interface IRepository<T> where T : IEntity
{
    /// <summary>
    /// Получить все записи
    /// </summary>
    Task<IList<T>> GetAllAsync();

    /// <summary>
    /// Получить запись по идентификатору
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавить запись
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Обновить запись
    /// </summary>
    Task<T?> UpdateAsync(T entity);

    /// <summary>
    /// Удалить запись
    /// </summary>
    Task<bool> DeleteAsync(int id);
}