namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория для выполнения CRUD-операций над сущностями.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Получает все сущности.
    /// </summary>
    /// <returns>Коллекция сущностей.</returns>
    public Task<IReadOnlyCollection<TEntity>> GetAllAsync();

    /// <summary>
    /// Получает сущность по IDу.
    /// </summary>
    /// <param name="id">ID сущности.</param>
    /// <returns>Сущность или null, если она не найдена.</returns>
    public Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет новую сущность.
    /// </summary>
    /// <param name="entity">Добавляемая сущность.</param>
    /// <returns>Добавленная сущность.</returns>
    public Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Обновляет существующую сущность.
    /// </summary>
    /// <param name="id">ID обновляемой сущности.</param>
    /// <param name="entity">Новые данные сущности.</param>
    /// <returns>true, если сущность найдена и обновлена; иначе false.</returns>
    public Task<bool> UpdateAsync(int id, TEntity entity);

    /// <summary>
    /// Удаляет сущность по IDу.
    /// </summary>
    /// <param name="id">ID удаляемой сущности.</param>
    /// <returns>true, если сущность найдена и удалена; иначе false.</returns>
    public Task<bool> DeleteAsync(int id);
}