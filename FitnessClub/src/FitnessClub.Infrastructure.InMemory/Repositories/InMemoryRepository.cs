using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.InMemory.Repositories;
public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly Lock _lock = new();
    private readonly List<TEntity> _items = [];
    private int _nextId = 1;
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult<IReadOnlyList<TEntity>>([.. _items]);
        }
    }
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult(_items.FirstOrDefault(e => e.Id == id));
        }
    }
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            entity.Id = _nextId++;
            _items.Add(entity);
            return Task.FromResult(entity);
        }
    }
    public Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            var index = _items.FindIndex(e => e.Id == entity.Id);
            if (index < 0)
                return Task.FromResult<TEntity?>(null);

            _items[index] = entity;
            return Task.FromResult<TEntity?>(entity);
        }
    }
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            var index = _items.FindIndex(e => e.Id == id);
            if (index < 0)
                return Task.FromResult(false);

            _items.RemoveAt(index);
            return Task.FromResult(true);
        }
    }
}