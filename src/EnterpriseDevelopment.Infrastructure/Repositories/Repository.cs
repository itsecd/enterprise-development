using EnterpriseDevelopment.Application.Repositories;

namespace EnterpriseDevelopment.Infrastructure.Repositories;

/// <summary>
/// Базовый репозиторий для хранения и управления сущностями
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    private readonly List<T> _items;
    private readonly Func<T, int> _getId;
    private readonly Action<T, int> _setId;

    /// <summary>
    /// Создаёт репозиторий с коллекцией сущностей
    /// </summary>
    public Repository(
        List<T> items,
        Func<T, int> getId,
        Action<T, int> setId)
    {
        _items = items;
        _getId = getId;
        _setId = setId;
    }

    /// <summary>
    /// Получает все сущности
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public IEnumerable<T> GetAll()
    {
        return _items;
    }

    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    /// <returns>Найденная сущность или null</returns>
    public T? GetById(int id)
    {
        return _items.FirstOrDefault(item => _getId(item) == id);
    }

    /// <summary>
    /// Добавляет новую сущность в хранилище
    /// </summary>
    /// <returns>Добавленная сущность</returns>
    public T Add(T entity)
    {
        var id = _items
            .Select(_getId)
            .DefaultIfEmpty(0)
            .Max() + 1;

        _setId(entity, id);
        _items.Add(entity);

        return entity;
    }

    /// <summary>
    /// Обновляет существующую сущность в хранилище
    /// </summary>
    public void Update(T entity)
    {
        var id = _getId(entity);
        var index = _items.FindIndex(item => _getId(item) == id);

        if (index < 0)
        {
            throw new KeyNotFoundException("Сущность не найдена");
        }

        _items[index] = entity;
    }

    /// <summary>
    /// Удаляет сущность из хранилища
    /// </summary>
    public void Delete(T entity)
    {
        var id = _getId(entity);
        var existing = GetById(id);

        if (existing != null)
        {
            _items.Remove(existing);
        }
    }
}
