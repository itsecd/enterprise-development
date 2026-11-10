namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Обобщенный интерфейс репозитория для базовых асинхронных операций
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Асинхронно получить все сущности
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Асинхронно получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Найденная сущность или пустое значение</returns>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Асинхронно добавить новую сущность
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    public Task AddAsync(T entity);

    /// <summary>
    /// Асинхронно обновить существующую сущность
    /// </summary>
    /// <param name="entity">Обновляемая сущность</param>
    public Task UpdateAsync(T entity);

    /// <summary>
    /// Асинхронно удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Признак успешного удаления</returns>
    public Task<bool> DeleteAsync(int id);
}