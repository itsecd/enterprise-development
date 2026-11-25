using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Tests.Fixtures.InMemory.Repositories;

/// <summary>
/// Общие операции с сущностями в памяти
/// </summary>
public abstract class InMemoryRepository<TEntity>(
    SalonStorage storage,
    List<TEntity> entities,
    Func<TEntity, TEntity> copy) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected SalonStorage Storage { get; } = storage;

    public IReadOnlyList<TEntity> GetAll()
    {
        lock (Storage.SyncRoot)
        {
            return entities.Select(copy).ToList();
        }
    }

    public TEntity? GetById(int id)
    {
        lock (Storage.SyncRoot)
        {
            var entity = Find(id);
            return entity is null ? null : copy(entity);
        }
    }

    public TEntity Add(TEntity entity)
    {
        return AddPrepared(() => PrepareCopy(entity, null));
    }

    public bool Update(TEntity entity)
    {
        return UpdatePrepared(entity.Id, current => PrepareCopy(entity, current));
    }

    /// <summary>
    /// Подготовить и добавить сущность под одной блокировкой
    /// </summary>
    protected TEntity AddPrepared(Func<TEntity> prepare)
    {
        lock (Storage.SyncRoot)
        {
            var saved = prepare();
            saved.Id = Storage.NextId<TEntity>();
            entities.Add(saved);
            return copy(saved);
        }
    }

    /// <summary>
    /// Найти и обновить сущность под одной блокировкой
    /// </summary>
    protected bool UpdatePrepared(int id, Func<TEntity, TEntity> prepare)
    {
        lock (Storage.SyncRoot)
        {
            var current = Find(id);
            if (current is null)
            {
                return false;
            }

            ApplyChanges(current, prepare(current));
            return true;
        }
    }

    private TEntity PrepareCopy(TEntity entity, TEntity? current)
    {
        var saved = copy(entity);
        PrepareForSave(saved, current);
        return saved;
    }

    public bool Delete(int id)
    {
        lock (Storage.SyncRoot)
        {
            var current = Find(id);
            if (current is null)
            {
                return false;
            }

            CheckDeletion(current);
            return entities.Remove(current);
        }
    }

    /// <summary>
    /// Подготовить копию к сохранению и проверить ограничения
    /// </summary>
    protected virtual void PrepareForSave(TEntity saved, TEntity? current)
    {
    }

    /// <summary>
    /// Обновить объект на месте, чтобы сохранить ссылки на него
    /// </summary>
    protected abstract void ApplyChanges(TEntity current, TEntity saved);

    /// <summary>
    /// Проверить, можно ли удалить объект
    /// </summary>
    protected virtual void CheckDeletion(TEntity entity)
    {
    }

    private TEntity? Find(int id)
    {
        return entities.FirstOrDefault(entity => entity.Id == id);
    }

    Task<IReadOnlyList<TEntity>> IRepository<TEntity>.GetAllAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(GetAll());
    }

    Task<TEntity?> IRepository<TEntity>.GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(GetById(id));
    }

    Task<TEntity> IRepository<TEntity>.AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Add(entity));
    }

    Task<bool> IRepository<TEntity>.UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Update(entity));
    }

    Task<bool> IRepository<TEntity>.DeleteAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Delete(id));
    }
}
