namespace AutoServiceLab.Domain.Repositories;

/// <summary>
/// Базовый интерфейс репозитория для CRUD-операций
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
/// <typeparam name="TId">Тип идентификатора</typeparam>
public interface IRepository<T, in TId> where T : class
{
    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    public T? Get(TId id);

    /// <summary>
    /// Получить все сущности
    /// </summary>
    public IReadOnlyList<T> GetAll();

    /// <summary>
    /// Добавить новую сущность
    /// </summary>
    public void Add(T entity);

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    public void Update(T entity);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    public bool Delete(TId id);
}
