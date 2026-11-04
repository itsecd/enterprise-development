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
    IList<T> GetAll();

    /// <summary>
    /// Получить запись по идентификатору
    /// </summary>
    T? GetById(int id);

    /// <summary>
    /// Добавить запись
    /// </summary>
    T Add(T entity);

    /// <summary>
    /// Обновить запись
    /// </summary>
    T? Update(T entity);

    /// <summary>
    /// Удалить запись
    /// </summary>
    bool Delete(int id);
}