using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Выполняет основные операции с записями в памяти
/// </summary>
public abstract class InMemoryRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly List<TEntity> _items;
    private readonly Func<TEntity, int> _getId;
    private readonly Action<TEntity, int> _setId;

    protected InMemoryRepository(
        List<TEntity> items,
        Func<TEntity, int> getId,
        Action<TEntity, int> setId)
    {
        _items = items;
        _getId = getId;
        _setId = setId;
    }

    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        IReadOnlyList<TEntity> result = _items.ToList();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Находит запись по идентификатору
    /// </summary>
    public Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = _items.FirstOrDefault(item => _getId(item) == id);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Добавляет новую запись и присваивает ей идентификатор
    /// </summary>
    public Task<TEntity> AddAsync(TEntity entity)
    {
        var nextId = _items.Count == 0 ? 1 : _items.Max(_getId) + 1;

        _setId(entity, nextId);
        _items.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет существующую запись
    /// </summary>
    public Task<bool> UpdateAsync(TEntity entity)
    {
        var id = _getId(entity);
        var index = _items.FindIndex(item => _getId(item) == id);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        _items[index] = entity;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = _items.FirstOrDefault(item => _getId(item) == id);

        if (entity is null)
        {
            return Task.FromResult(false);
        }

        _items.Remove(entity);

        return Task.FromResult(true);
    }
}
