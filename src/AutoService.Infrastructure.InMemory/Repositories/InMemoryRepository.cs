using System.Linq.Expressions;
using AutoService.Domain.Exceptions;
using AutoService.Domain.Interfaces;

namespace AutoService.Infrastructure.InMemory.Repositories;

/// <summary>Коллекция с монотонным счётчиком идентификаторов. Используется внутри сеанса.</summary>
/// <typeparam name="T">Тип хранимой сущности.</typeparam>
public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly Lock _gate = new();
    private readonly Dictionary<int, T> _entities;
    private int _lastId;

    /// <summary>Загружает начальные записи и устанавливает счётчик.</summary>
    public InMemoryRepository(IEnumerable<T> entities)
    {
        _entities = entities.ToDictionary(entity => entity.Id);
        _lastId = _entities.Keys.DefaultIfEmpty().Max();
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<T>> GetAllAsync()
    {
        lock (_gate)
        {
            return Task.FromResult<IReadOnlyList<T>>([.. _entities.Values.OrderBy(entity => entity.Id)]);
        }
    }

    /// <inheritdoc />
    public Task<T?> GetByIdAsync(int id)
    {
        lock (_gate)
        {
            return Task.FromResult(_entities.GetValueOrDefault(id));
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyCollection<int> ids)
    {
        lock (_gate)
        {
            return Task.FromResult<IReadOnlyList<T>>([.. ids.Distinct().Where(_entities.ContainsKey).Select(id => _entities[id])]);
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        lock (_gate)
        {
            return Task.FromResult<IReadOnlyList<T>>([.. _entities.Values.Where(predicate.Compile()).OrderBy(entity => entity.Id)]);
        }
    }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        lock (_gate)
        {
            return Task.FromResult(_entities.Values.Any(predicate.Compile()));
        }
    }

    /// <inheritdoc />
    public Task<T> AddAsync(T entity)
    {
        lock (_gate)
        {
            entity.Id = checked(++_lastId);
            _entities.Add(entity.Id, entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc />
    public Task UpdateAsync(int id, Action<T> update)
    {
        lock (_gate)
        {
            update(GetRequired(id));
            return Task.CompletedTask;
        }
    }

    /// <inheritdoc />
    public Task DeleteAsync(int id)
    {
        lock (_gate)
        {
            _ = GetRequired(id);
            _entities.Remove(id);
            return Task.CompletedTask;
        }
    }

    private T GetRequired(int id) => _entities.GetValueOrDefault(id)
        ?? throw new EntityNotFoundException($"Запись с идентификатором {id} не найдена.");
}
