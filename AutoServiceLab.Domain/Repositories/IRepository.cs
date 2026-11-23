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
    public Task<T?> GetAsync(TId id);

    /// <summary>
    /// Получить все сущности
    /// </summary>
    public Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Добавить новую сущность
    /// </summary>
    public Task AddAsync(T entity);

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    public Task UpdateAsync(T entity);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(TId id);
}
