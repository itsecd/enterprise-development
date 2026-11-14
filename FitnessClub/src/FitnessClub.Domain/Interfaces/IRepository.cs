using FitnessClub.Domain.Entities;
namespace FitnessClub.Domain.Interfaces;
/// <summary>
/// Базовые операции с сущностями
/// </summary>
public interface IRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список всех сущностей</returns>
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Сущность или null, если не найдена</returns>
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Добавить сущность
    /// </summary>
    /// <param name="entity">Сущность для добавления</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Добавленная сущность</returns>
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновить сущность
    /// </summary>
    /// <param name="entity">Сущность для обновления</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Обновленная сущность</returns>
    public Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности для удаления</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Удалил или нет</returns>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}