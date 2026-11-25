using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Общие операции с сущностями
/// </summary>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Получить все объекты
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Найти объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Найденный объект или null</returns>
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить объект и назначить ему идентификатор
    /// </summary>
    /// <param name="entity">Добавляемый объект</param>
    /// <returns>Сохранённый объект</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    /// <param name="entity">Новые данные с идентификатором объекта</param>
    /// <returns>Признак успешного обновления</returns>
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Признак успешного удаления</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
