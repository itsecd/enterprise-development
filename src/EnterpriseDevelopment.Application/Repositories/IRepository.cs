namespace EnterpriseDevelopment.Application.Repositories;

/// <summary>
/// Общий интерфейс репозитория для хранения и получения сущностей
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получает все сущности
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public IEnumerable<T> GetAll();

    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    /// <returns>Найденная сущность или null</returns>
    public T? GetById(int id);

    /// <summary>
    /// Добавляет новую сущность
    /// </summary>
    /// <returns>Добавленная сущность</returns>
    public T Add(T entity);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    public void Update(T entity);

    /// <summary>
    /// Удаляет существующую сущность
    /// </summary>
    public void Delete(T entity);
}
