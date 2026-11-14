namespace CarWash.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    /// <returns>Список сущностей</returns>
    public Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Возвращает сущность по ID
    /// </summary>
    /// <param name="id">ID сущности</param>
    /// <returns>Сущность или null, если она не найдена</returns>
    public Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет новую сущность
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <returns>Добавленная сущность с присвоенным ID</returns>
    public Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Обновляет сущность
    /// </summary>
    /// <param name="entity">Обновлённая сущность</param>
    public Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Удаляет сущность по ID
    /// </summary>
    /// <param name="id">ID сущности</param>
    public Task DeleteAsync(int id);
}
