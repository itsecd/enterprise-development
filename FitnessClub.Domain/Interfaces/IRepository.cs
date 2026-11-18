namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Универсальный контракт репозитория
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Добавляет сущность, назначает ей id и возвращает её
    /// </summary>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    /// <returns></returns>
    public Task<IReadOnlyList<TEntity>> ReadAll();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public Task<TEntity?> Read(int entityId);

    /// <summary>
    /// Обновляет сущность
    /// </summary>
    public Task<bool> Update(TEntity entity);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    public Task<bool> Delete(int entityId);
}
