namespace Carwash.Domain;

/// <summary>
/// Интерфейс репозитория для CRUD операций с сущностями
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Создание новой сущности
    /// </summary>
    /// <param name="entity">Новая сущность</param>
    /// <returns>Сущность</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>Сущность</returns>
    public Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Получение списка всех сущностей
    /// </summary>
    /// <returns>Список сущностей</returns>
    public Task<List<TEntity>> ReadAll();

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity">Обновленная сущность</param>
    /// <returns>Обновленная сущность</returns>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>True, если сущность была удалена, иначе False</returns>
    public Task<bool> Delete(TKey entityId);
}
