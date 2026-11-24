namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Базовый шаблонный интерфейс репозитория для работы с сущностью.
/// </summary>
public interface IRepository<T>
{
    /// <summary>Получить все сущности.</summary>
    public Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор сущности.</param>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавить новую сущность.
    /// </summary>
    /// <param name="entity">Сущность для добвления.</param>
    public Task AddAsync(T entity);

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    /// <param name="entity">Сущность для обновления.</param>
    public Task UpdateAsync(T entity);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="entity">Сущность для удаления.</param>
    public Task DeleteAsync(T entity);
}