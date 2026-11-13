using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Предоставляет общую реализацию CRUD-операций для хранения доменных сущностей в памяти
/// </summary>
public abstract class InMemoryRepository<T>(
    IList<T> entities)
    : IRepository<T>
    where T : IEntity
{
    /// <inheritdoc />
    public Task<IList<T>> GetAllAsync()
    {
        return Task.FromResult(entities);
    }

    /// <inheritdoc />
    public Task<T?> GetByIdAsync(int id)
    {
        var entity = entities.FirstOrDefault(entity => entity.Id == id);

        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task<T> AddAsync(T entity)
    {
        entity.Id = entities.Count == 0
            ? 1
            : entities.Max(item => item.Id) + 1;

        entities.Add(entity);

        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public abstract Task<T?> UpdateAsync(T entity);

    /// <inheritdoc />
    public virtual Task<bool> DeleteAsync(int id)
    {
        var entity = entities.FirstOrDefault(entity => entity.Id == id);

        if (entity is null)
        {
            return Task.FromResult(false);
        }

        entities.Remove(entity);

        return Task.FromResult(true);
    }
}