using VetClinic.Domain.Interfaces;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория для хранения данных в памяти
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly List<T> _items;
    private readonly Lock _lock = new();
    private int _currentId;

    /// <summary>
    /// Инициализация репозитория начальными данными
    /// </summary>
    /// <param name="initialData">Коллекция начальных данных</param>
    public InMemoryRepository(IEnumerable<T> initialData)
    {
        _items = [.. initialData];
        _currentId = _items.Count > 0 ? _items.Max(x => x.Id) : 0;
    }

    /// <inheritdoc />
    public Task<IEnumerable<T>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult<IEnumerable<T>>([.. _items]);
        }
    }

    /// <inheritdoc />
    public Task<T?> GetByIdAsync(int id)
    {
        lock (_lock)
        {
            return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }
    }

    /// <inheritdoc />
    public Task AddAsync(T entity)
    {
        lock (_lock)
        {
            entity.Id = ++_currentId;
            _items.Add(entity);
            return Task.CompletedTask;
        }
    }

    /// <inheritdoc />
    public Task UpdateAsync(T entity)
    {
        lock (_lock)
        {
            var index = _items.FindIndex(x => x.Id == entity.Id);
            if (index != -1)
            {
                _items[index] = entity;
            }

            return Task.CompletedTask;
        }
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item is not null && _items.Remove(item));
        }
    }
}