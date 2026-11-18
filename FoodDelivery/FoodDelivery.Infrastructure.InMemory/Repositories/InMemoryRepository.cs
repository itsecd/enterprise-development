using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализует общие операции хранения сущностей в памяти
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
/// <param name="entities">Коллекция сущностей приложения</param>
/// <param name="getId">Функция получения идентификатора</param>
/// <param name="syncRoot">Синхронизация общего графа данных</param>
public abstract class InMemoryRepository<TEntity>(
    List<TEntity> entities,
    Func<TEntity, int> getId,
    Lock syncRoot) : IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Объект синхронизации операций репозитория
    /// </summary>
    protected Lock SyncRoot { get; } = syncRoot;

    private int _lastId = entities
        .Select(getId)
        .DefaultIfEmpty()
        .Max();

    /// <inheritdoc/>
    public IReadOnlyList<TEntity> GetAll()
    {
        lock (SyncRoot)
        {
            return [.. entities];
        }
    }

    /// <inheritdoc/>
    public TEntity? GetById(int id)
    {
        lock (SyncRoot)
        {
            return entities.FirstOrDefault(entity => getId(entity) == id);
        }
    }

    /// <inheritdoc/>
    public TEntity Add(TEntity entity)
    {
        lock (SyncRoot)
        {
            var nextId = checked(_lastId + 1);
            SetId(entity, nextId);
            entities.Add(entity);
            _lastId = nextId;

            return entity;
        }
    }

    /// <inheritdoc/>
    public bool Update(TEntity entity)
    {
        lock (SyncRoot)
        {
            var id = getId(entity);
            var existingEntity = entities
                .FirstOrDefault(item => getId(item) == id);

            if (existingEntity is null)
            {
                return false;
            }

            UpdateEntity(existingEntity, entity);

            return true;
        }
    }

    /// <inheritdoc/>
    public void Delete(int id)
    {
        lock (SyncRoot)
        {
            entities.RemoveAll(entity => getId(entity) == id);
        }
    }

    /// <inheritdoc/>
    public T Execute<T>(Func<T> operation)
    {
        lock (SyncRoot)
        {
            return operation();
        }
    }

    /// <inheritdoc/>
    public void Execute(Action operation)
    {
        lock (SyncRoot)
        {
            operation();
        }
    }

    /// <summary>
    /// Назначает идентификатор сущности
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <param name="id">Идентификатор</param>
    protected abstract void SetId(TEntity entity, int id);

    /// <summary>
    /// Переносит новые значения в существующую сущность
    /// </summary>
    /// <param name="existingEntity">Существующая сущность</param>
    /// <param name="updatedEntity">Сущность с новыми значениями</param>
    protected abstract void UpdateEntity(
        TEntity existingEntity,
        TEntity updatedEntity);
}
