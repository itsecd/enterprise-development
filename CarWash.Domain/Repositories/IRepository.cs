using System.Linq.Expressions;
using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Базовые операции с коллекцией сущностей
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить наличие сущности по условию
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить сущность и назначить свободный идентификатор
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
