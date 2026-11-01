using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory;

/// <summary>
/// Репозиторий с хранением в памяти и последовательным выделением идентификаторов.
/// </summary>
/// <param name="seed">Начальные данные.</param>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public abstract class InMemoryRepository<TEntity>(IReadOnlyList<TEntity> seed) : IRepository<TEntity>
    where TEntity : class, IIdentified
{
    private readonly List<TEntity> _items = [.. seed];
    private readonly Lock _lock = new();
    private int _nextId = seed.Count == 0 ? 0 : seed.Max(item => item.Id) + 1;

    /// <inheritdoc/>
    public Task<IReadOnlyList<TEntity>> GetAllAsync() =>
        ReadAsync<IReadOnlyList<TEntity>>(items => [.. items]);

    /// <inheritdoc/>
    public Task<TEntity?> GetByIdAsync(int id) =>
        ReadAsync(items => items.FirstOrDefault(item => item.Id == id));

    /// <inheritdoc/>
    public Task<TEntity> AddAsync(TEntity entity)
    {
        lock (_lock)
        {
            if (_items.Any(item => ReferenceEquals(item, entity)))
            {
                throw new InvalidOperationException("Объект уже добавлен в репозиторий.");
            }

            entity.Id = _nextId++;
            _items.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc/>
    public Task<bool> UpdateAsync(int id, TEntity entity)
    {
        lock (_lock)
        {
            var existing = _items.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return Task.FromResult(false);
            }

            CopyProperties(entity, existing);
            return Task.FromResult(true);
        }
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            return Task.FromResult(_items.RemoveAll(item => item.Id == id) > 0);
        }
    }

    /// <summary>
    /// Выполняет запрос под блокировкой, пока коллекция не меняется.
    /// </summary>
    protected Task<TResult> ReadAsync<TResult>(Func<IReadOnlyList<TEntity>, TResult> query)
    {
        lock (_lock)
        {
            return Task.FromResult(query(_items));
        }
    }

    /// <summary>
    /// Копирует изменяемые свойства, сохраняя идентификатор и ссылки на существующий объект.
    /// </summary>
    protected abstract void CopyProperties(TEntity source, TEntity target);
}
