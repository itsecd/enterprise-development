using VetClinic.Domain.Repositories;
using VetClinic.Domain.Seeds;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Базовый репозиторий, хранящий сущности в коллекции в памяти
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <param name="seed">Начальные данные, общие для всех репозиториев</param>
/// <param name="items">Коллекция сущностей этого репозитория</param>
public abstract class InMemoryRepository<TEntity>(VetClinicSeed seed, List<TEntity> items) : IRepository<TEntity>
    where TEntity : class
{
    private readonly Lock _lock = new();

    /// <summary>
    /// Начальные данные, общие для всех репозиториев
    /// </summary>
    protected VetClinicSeed Seed { get; } = seed;

    /// <summary>
    /// Получить идентификатор сущности
    /// </summary>
    /// <param name="entity">Сущность</param>
    protected abstract int GetId(TEntity entity);

    /// <summary>
    /// Присвоить сущности идентификатор
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <param name="id">Новый идентификатор</param>
    protected abstract void SetId(TEntity entity, int id);

    /// <inheritdoc/>
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            return Task.FromResult<IReadOnlyList<TEntity>>([.. items]);
        }
    }

    /// <inheritdoc/>
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            return Task.FromResult(items.Find(item => GetId(item) == id));
        }
    }

    /// <inheritdoc/>
    public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            SetId(entity, items.Count == 0 ? 1 : items.Max(GetId) + 1);
            items.Add(entity);
        }
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Сущности хранятся по ссылке, поэтому изменения уже находятся в коллекции
    /// </remarks>
    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            return Task.FromResult(items.RemoveAll(item => GetId(item) == id) > 0);
        }
    }
}
