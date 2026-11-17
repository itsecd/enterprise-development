namespace VetClinic.Domain.Repositories;

/// <summary>
/// Базовые CRUD-операции над сущностью
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить сущность
    /// </summary>
    /// <param name="entity">Новая сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Сохранить изменения сущности, полученной из репозитория
    /// </summary>
    /// <param name="entity">Изменённая сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
