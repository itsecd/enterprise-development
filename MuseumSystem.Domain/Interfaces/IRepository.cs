namespace MuseumSystem.Domain.Interfaces;

/// <summary>
/// Обобщенный интерфейс репозитория для работы с сущностями
/// </summary>
/// <typeparam name="TEntity">Тип сущности, наследующий IEntity</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Получить список всех сущностей
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Получить сущность по её идентификатору
    /// </summary>
    /// <param name="id">Уникальный идентификатор сущности</param>
    /// <returns>Найденная сущность или null, если не найдена</returns>
    public Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Добавить новую сущность в хранилище
    /// </summary>
    /// <param name="entity">Объект новой сущности</param>
    /// <returns>Созданная сущность (с присвоенным Id)</returns>
    public Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    /// <param name="entity">Объект сущности с обновленными данными</param>
    /// <returns>Обновленная сущность или null, если сущность не найдена</returns>
    public Task<TEntity?> UpdateAsync(TEntity entity);

    /// <summary>
    /// Удалить сущность по её идентификатору
    /// </summary>
    /// <param name="id">Уникальный идентификатор сущности</param>
    /// <returns>True, если удаление прошло успешно, иначе False</returns>
    public Task<bool> DeleteAsync(int id);
}