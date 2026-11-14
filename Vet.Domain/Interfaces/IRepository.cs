namespace Vet.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория для выполнения стандартных CRUD-операций
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Асинхронное получение всех сущностей
    /// </summary>
    /// <returns>Список всех сущностей</returns>
    public Task<IReadOnlyList<TEntity>> GetAllAsync();

    /// <summary>
    /// Асинхронный поиск сущности по ID
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>Найденная сущность, или null, если не найдена</returns>
    public Task<TEntity?> GetByIdAsync(TKey entityId);

    /// <summary>
    /// Асинхронное добавление новой сущности
    /// </summary>
    /// <param name="entity">Экземпляр сущности для добавления</param>
    /// <returns>Добавленная сущность</returns>
    public Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Асинхронное обновление существующей сущности
    /// </summary>
    /// <param name="entity">Сущность, подлежащая обновлению</param>
    /// <returns>True, если сущность обновлена; False, если не найдена</returns>
    public Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// Асинхронное удаление сущности по ID
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>True, если сущность удалена; False, если не найдена</returns>
    public Task<bool> DeleteAsync(TKey entityId);
}
