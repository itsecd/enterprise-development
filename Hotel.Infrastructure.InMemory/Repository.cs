using Hotel.Domain.Interfaces;
using System.Linq.Expressions;

namespace Hotel.Infrastructure.InMemory;

/// <summary>
/// Реализация интерфейса репозитория с использованием асинхронных методов
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly List<T> _items;
    private readonly Lock _lock = new();
    private int _lastId;

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    /// <param name="initialData">Инициализация сущностей</param>
    public InMemoryRepository(IEnumerable<T> initialData)
    {
        _items = initialData.ToList();
        if (_items.Count > 0) _lastId = _items.Max(x => x.Id);
        else _lastId = 0;
    }

    /// <summary>
    /// Получить все сущности из памяти
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public Task<IReadOnlyList<T>> GetAllAsync()
    {
        lock (_lock) return Task.FromResult<IReadOnlyList<T>>(_items.ToList());
    }

    /// <summary>
    /// Получить одну сущность
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Сущность</returns>
    public Task<T?> GetByIdAsync(int id)
    {
        lock (_lock) return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
    }

    /// <summary>
    /// Добавить сущность в память
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <returns>Добавленная сущность</returns>
    public Task<T> AddAsync(T entity)
    {
        lock (_lock)
        {
            entity.Id = ++_lastId;
            _items.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <summary>
    /// Обновить значение сущности
    /// </summary>
    /// <param name="id">Id обновляемой сущности</param>
    /// <param name="update">Обновляющее действие</param>
    /// <exception cref="KeyNotFoundException">Сущность не найдена</exception>
    /// <returns>True, если сущность найдена и обновлена, иначе False</returns>
    public Task<bool> UpdateAsync(int id, Action<T> update)
    {
        lock (_lock)
        {
            var entity = _items.FirstOrDefault(x => x.Id == id);
            if (entity == null) return Task.FromResult(false);

            update(entity);
            return Task.FromResult(true);
        }
    }

    /// <summary>
    /// Удалить сущность
    /// </summary>
    /// <param name="id">Id удаляемой сущности</param>
    /// <exception cref="KeyNotFoundException">Сущность не найдена</exception>
    public Task DeleteAsync(int id)
    {
        lock (_lock)
        {
            var entity = _items.FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new KeyNotFoundException($"Сущность(Id={id}) не найдена.");
            _items.Remove(entity);
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Получение сущностей по условию
    /// </summary>
    public Task<IReadOnlyList<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        lock (_lock)
        {
            return Task.FromResult<IReadOnlyList<T>>(_items.Where(predicate.Compile()).ToList());
        }
    }
}