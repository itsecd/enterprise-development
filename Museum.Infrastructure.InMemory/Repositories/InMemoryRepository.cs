using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Выполняет основные операции с записями в памяти
/// </summary>
public abstract class InMemoryRepository<TEntity>(
    List<TEntity> items,
    Func<TEntity, int> getId,
    Action<TEntity, int> setId) : IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        IReadOnlyList<TEntity> result = items.ToList();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Находит запись по идентификатору
    /// </summary>
    public Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = items.FirstOrDefault(item => getId(item) == id);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Добавляет новую запись и присваивает ей идентификатор
    /// </summary>
    public Task<TEntity> AddAsync(TEntity entity)
    {
        var nextId = items.Count == 0 ? 1 : items.Max(getId) + 1;

        setId(entity, nextId);
        items.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет существующую запись
    /// </summary>
    public Task<bool> UpdateAsync(TEntity entity)
    {
        var id = getId(entity);
        var index = items.FindIndex(item => getId(item) == id);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        items[index] = entity;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = items.FirstOrDefault(item => getId(item) == id);

        if (entity is null)
        {
            return Task.FromResult(false);
        }

        items.Remove(entity);

        return Task.FromResult(true);
    }
}
