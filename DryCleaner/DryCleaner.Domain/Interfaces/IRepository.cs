namespace DryCleaner.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория
/// </summary>
/// <typeparam name="TEntity">Тип данных сущности репозитория</typeparam>
/// <typeparam name="TKey">Тип ключа сущности репозитория</typeparam>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Асинхронно созданет сущность
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде созданной сущности</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Асинхронно получает сущность по заданному идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор сущности.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде найденной сущности.</returns>
    public Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Асинхронно возвращает все сущности типа TEntity.
    /// </summary>
    /// <returns>Список всех сущностей типа TEntity.</returns>
    public Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Асинхронно обновляет значение сущности
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде сущности с обновленными данными</returns>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Асинхронно удаляет сущность
    /// </summary>
    /// <param name="entityId">Id сущности</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом характиризующем успешность удаления</returns>
    public Task<bool> Delete(TKey entityId);

}
