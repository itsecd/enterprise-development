namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет операции хранения доменных сущностей
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Сущность или null, если она не найдена</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет сущность и назначает ей идентификатор
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <returns>Добавленная сущность</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    /// <param name="entity">Сущность с новыми значениями свойств</param>
    /// <returns>Признак успешного обновления</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сущность, если она существует
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
