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
    public IList<T> GetAll() => entities.ToList();

    /// <inheritdoc />
    public T? GetById(int id) => entities.FirstOrDefault(entity => entity.Id == id);

    /// <inheritdoc />
    public T Add(T entity)
    {
        entity.Id = entities.Count == 0 ? 1 : entities.Max(item => item.Id) + 1;

        entities.Add(entity);
        return entity;
    }

    /// <inheritdoc />
    public abstract T? Update(T entity);

    /// <inheritdoc />
    public virtual bool Delete(int id)
    {
        var entity = GetById(id);

        if (entity is null)
        {
            return false;
        }

        return entities.Remove(entity);
    }
}