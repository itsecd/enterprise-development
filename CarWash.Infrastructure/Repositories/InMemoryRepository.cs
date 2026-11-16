using System.Collections.Concurrent;
using System.Linq.Expressions;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Базовый репозиторий в памяти для тестов
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public abstract class InMemoryRepository<TEntity>(IEnumerable<TEntity> entities)
    : IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Коллекция сущностей по идентификаторам
    /// </summary>
    protected ConcurrentDictionary<int, TEntity> Items { get; } =
        new(entities.ToDictionary(entity => entity.Id));

    /// <inheritdoc />
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<TEntity>>(Items.Values.OrderBy(entity => entity.Id).ToList());

    /// <inheritdoc />
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.GetValueOrDefault(id));

    /// <inheritdoc />
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.Values.Any(predicate.Compile()));

    /// <inheritdoc />
    public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var id = Items.Count + 1;
        while (true)
        {
            entity.Id = id;
            if (Items.TryAdd(id, entity))
                return Task.CompletedTask;
            id = checked(id + 1);
        }
    }

    /// <inheritdoc />
    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (Items.TryGetValue(entity.Id, out var current))
            Items.TryUpdate(entity.Id, entity, current);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        Items.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}
