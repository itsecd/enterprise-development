namespace Hotel.Domain;

/// <summary>
/// Обобщённый интерфейс репозитория
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<TEntity?> GetById(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить новую сущность
    /// </summary>
    /// <param name="entity">Новая сущность</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    /// <param name="entity">Обновлённая сущность</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<TEntity?> Update(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<bool> Delete(TKey id, CancellationToken cancellationToken = default);
}