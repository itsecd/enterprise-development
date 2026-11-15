namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Базовый контракт репозитория для работы с сущностями
/// </summary>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    Task<TEntity?> Get(TKey id);
    /// <summary>
    /// Получает все сущности
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAll();
    /// <summary>
    /// Добавляет сущность
    /// </summary>
    Task Add(TEntity entity);
    /// <summary>
    /// Обновляет сущность
    /// </summary>
    Task Update(TEntity entity);
    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    Task<bool> Delete(TKey id);
}