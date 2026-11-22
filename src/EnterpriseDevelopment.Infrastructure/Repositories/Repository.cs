using EnterpriseDevelopment.Application.Repositories;

namespace EnterpriseDevelopment.Infrastructure.Repositories;

/// <summary>
/// Базовый репозиторий для хранения и управления сущностями
/// </summary>
public class Repository<T>(
    List<T> items,
    Func<T, int> getId,
    Action<T, int> setId) : IRepository<T> where T : class
{
    /// <summary>
    /// Получает все сущности
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public IEnumerable<T> GetAll()
    {
        return items;
    }

    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    /// <returns>Найденная сущность или null</returns>
    public T? GetById(int id)
    {
        return items.FirstOrDefault(item => getId(item) == id);
    }

    /// <summary>
    /// Добавляет новую сущность в хранилище
    /// </summary>
    /// <returns>Добавленная сущность</returns>
    public T Add(T entity)
    {
        var id = items
            .Select(getId)
            .DefaultIfEmpty(0)
            .Max() + 1;

        setId(entity, id);
        items.Add(entity);

        return entity;
    }

    /// <summary>
    /// Обновляет существующую сущность в хранилище
    /// </summary>
    public void Update(T entity)
    {
        var id = getId(entity);
        var index = items.FindIndex(item => getId(item) == id);

        if (index < 0)
        {
            throw new KeyNotFoundException("Сущность не найдена");
        }

        items[index] = entity;
    }

    /// <summary>
    /// Удаляет сущность из хранилища
    /// </summary>
    public void Delete(T entity)
    {
        var id = getId(entity);
        var existing = GetById(id);

        if (existing != null)
        {
            items.Remove(existing);
        }
    }
}
