namespace Hotel.Domain;

/// <summary>
/// Интерфейс репозитория для CRUD операций
/// </summary>
/// <typeparam name="TEntity">Сущность</typeparam>
/// <typeparam name="TKey">Первичный ключ</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Создание сущности
    /// </summary>
    /// <param name="entity">Создаваемая сущность</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Сущность</returns>
    public Task<TEntity> Create(TEntity entity, CancellationToken ct = default);
    
    /// <summary>
    /// Получение сущности по ключу
    /// </summary>
    /// <param name="entityId">Id сущности</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Сущность</returns>
    public Task<TEntity?> Read(TKey entityId, CancellationToken ct = default);
    
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Коллекция сущностей</returns>
    public Task<IList<TEntity>> ReadAll(CancellationToken ct = default);
    
    /// <summary>
    /// Замена сущности
    /// </summary>
    /// <param name="entity">Заменяющая сущность</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Сущность</returns>
    public Task<TEntity> Update(TEntity entity, CancellationToken ct = default);
    
    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="entityId">Id сущности</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Успешность удаления</returns>
    public Task<bool> Delete(TKey entityId, CancellationToken ct = default);
}