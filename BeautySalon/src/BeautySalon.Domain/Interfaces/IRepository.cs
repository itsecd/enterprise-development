namespace BeautySalon.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория с CRUD-операциями над сущностью
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Получает все сущности
    /// </summary>
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает сущность по идентификатору или <c>null</c>, если она не найдена
    /// </summary>
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет новую сущность. Если идентификатор не задан (0), он назначается автоматически
    /// </summary>
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Сохраняет изменения существующей сущности
    /// </summary>
    /// <returns><c>true</c>, если сущность найдена и обновлена</returns>
    public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <returns><c>true</c>, если сущность найдена и удалена</returns>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}