using MuseumSystem.Domain.Interfaces;

namespace MuseumSystem.Infrastructure.InMemory.Repositories;

/// <summary>
/// Обобщенная реализация репозитория в памяти
/// </summary>
public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly List<TEntity> _items;
    private readonly Lock _lock = new();
    private int _nextId;

    public InMemoryRepository(IEnumerable<TEntity> seedData)
    {
        _items = seedData.ToList();
        _nextId = _items.Count > 0 ? _items.Max(e => e.Id) + 1 : 1;
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        lock (_lock) return Task.FromResult<IEnumerable<TEntity>>([.. _items]);
    }

    public Task<TEntity?> GetByIdAsync(int id)
    {
        lock (_lock) return Task.FromResult(_items.FirstOrDefault(e => e.Id == id));
    }

    public Task<TEntity> AddAsync(TEntity entity)
    {
        lock (_lock)
        {
            entity.Id = _nextId++;
            _items.Add(entity);
            return Task.FromResult(entity);
        }
    }

    public Task<TEntity?> UpdateAsync(TEntity entity)
    {
        lock (_lock)
        {
            var existing = _items.FirstOrDefault(e => e.Id == entity.Id);
            if (existing == null) return Task.FromResult<TEntity?>(null);

            var index = _items.IndexOf(existing);
            _items[index] = entity;

            return Task.FromResult<TEntity?>(entity);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var existing = _items.FirstOrDefault(e => e.Id == id);
            if (existing == null) return Task.FromResult(false);

            _items.Remove(existing);
            return Task.FromResult(true);
        }
    }
}