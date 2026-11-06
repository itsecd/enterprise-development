
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Базовый репозиторий для хранения сущностей в памяти.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public abstract class InMemoryRepository<T> : IRepository<T>
    where T : class
{
    protected readonly List<T> Items;
    protected readonly object SyncRoot = new();

    private readonly Func<T, int> _getId;
    private readonly Action<T, int> _setId;
    private int _nextId;

    protected InMemoryRepository(
        List<T> items,
        Func<T, int> getId,
        Action<T, int> setId)
    {
        Items = items;
        _getId = getId;
        _setId = setId;

        _nextId = items.Count == 0
            ? 1
            : items.Max(getId) + 1;
    }

    /// <summary>
    /// Получить все сущности.
    /// </summary>
    public IReadOnlyList<T> GetAll()
    {
        lock (SyncRoot)
        {
            return [.. Items];
        }
    }

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    public T? GetById(int id)
    {
        lock (SyncRoot)
        {
            return Items.FirstOrDefault(
                item => _getId(item) == id);
        }
    }

    /// <summary>
    /// Добавить новую сущность.
    /// </summary>
    public T Add(T entity)
    {
        lock (SyncRoot)
        {
            _setId(entity, _nextId++);
            Items.Add(entity);

            return entity;
        }
    }

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    public abstract bool Update(int id, T entity);

    /// <summary>
    /// Удалить сущность по идентификатору.
    /// </summary>
    public virtual bool Delete(int id)
    {
        lock (SyncRoot)
        {
            var entity = Items.FirstOrDefault(
                item => _getId(item) == id);

            return entity is not null && Items.Remove(entity);
        }
    }
}
