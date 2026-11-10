using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Базовый репозиторий поверх коллекции в памяти. Потокобезопасен.
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
public abstract class InMemoryRepository<TEntity>(List<TEntity> items) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly Lock _lock = new();

    /// <inheritdoc />
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            return Task.FromResult<IReadOnlyList<TEntity>>([.. items]);
        }
    }

    /// <inheritdoc />
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            return Task.FromResult(items.FirstOrDefault(x => x.Id == id));
        }
    }

    /// <inheritdoc />
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            if (entity.Id == 0)
            {
                entity.Id = items.Count != 0 ? items.Max(x => x.Id) + 1 : 1;
            }
            else if (items.Any(x => x.Id == entity.Id))
            {
                throw new InvalidOperationException($"Сущность с идентификатором {entity.Id} уже существует.");
            }

            items.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var index = items.FindIndex(x => x.Id == entity.Id);
            if (index == -1)
            {
                return Task.FromResult(false);
            }

            items[index] = entity;
            return Task.FromResult(true);
        }
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item is not null && items.Remove(item));
        }
    }
}