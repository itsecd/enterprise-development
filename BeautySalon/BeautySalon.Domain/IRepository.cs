namespace BeautySalon.Domain;

/// <summary>
/// Интерфейс репозитория CRUD операций
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип Id сущности</typeparam>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Создает сущность
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns>Созданная сущность</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Получает сущность
    /// </summary>
    /// <param name="entityId">Id сущности</param>
    /// <returns>Полученная сущность</returns>
    public Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Получает все сущности
    /// </summary>
    /// <returns>Список полученных сущностей</returns>
    public Task<List<TEntity>> ReadAll();

    /// <summary>
    /// Обновляет сущность
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns>Обновленная сущность</returns>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удаляет сущность
    /// </summary>
    /// <param name="entityId">Id сущности</param>
    /// <returns>Результат выполнения удаления</returns>
    public Task<bool> Delete(TKey entityId);
}
