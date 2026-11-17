using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Общая реализация CRUD-операций для хранения доменных сущностей в памяти
/// </summary>
public abstract class InMemoryRepository<T>(IList<T> entities) : IRepository<T>
    where T : IEntity
{
    /// <inheritdoc />
    public Task<IList<T>> GetAllAsync() => Task.FromResult<IList<T>>(entities.ToList());

    /// <inheritdoc />
    public Task<T?> GetByIdAsync(int id) => Task.FromResult(entities.FirstOrDefault(entity => entity.Id == id));

    /// <inheritdoc />
    public Task<T> AddAsync(T entity)
    {
        entity.Id = entities.Count == 0 ? 1 : entities.Max(item => item.Id) + 1;

        entities.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public abstract Task<T?> UpdateAsync(T entity);

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        var entity = entities.FirstOrDefault(e => e.Id == id);

        if (entity is null) return Task.FromResult(false);

        entities.Remove(entity);

        return Task.FromResult(true);
    }
}