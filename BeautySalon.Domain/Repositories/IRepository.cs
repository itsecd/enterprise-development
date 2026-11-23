namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Базовый шаблонный интерфейс репозитория для работы с сущностью.
/// </summary>
public interface IRepository<T>
{
    /// <summary>Получить все сущности.</summary>
    public IEnumerable<T> GetAll();

    /// <summary>Получить сущность по идентификатору.</summary>
    public T? GetById(int id);

    /// <summary>Добавить новую сущность.</summary>
    public void Add(T entity);

    /// <summary>Обновить существующую сущность.</summary>
    public void Update(T entity);

    /// <summary>Удалить сущность.</summary>
    public void Delete(T entity);
}