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
    /// <returns>
    /// Список всех сущностей
    /// </returns>
    public Task<IReadOnlyList<TEntity>> GetAllAsync();
    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">
    /// Идентификатор сущности
    /// </param>
    /// <returns>
    /// Сущность или null, если не найдена
    /// </returns>
    public Task<TEntity?> GetByIdAsync(int id);
    /// <summary>
    /// Добавить сущность
    /// </summary>
    /// <param name="entity">
    /// Сущность для добавления
    /// </param>
    /// <returns>
    /// Добавленная сущность
    /// </returns>
    public Task<TEntity> AddAsync(TEntity entity);
    /// <summary>
    /// Обновить сущность
    /// </summary>
    /// <param name="entity">
    /// Сущность для обновления
    /// </param>
    /// <returns>
    /// Обновленная сущность
    /// </returns>
    public Task<TEntity?> UpdateAsync(TEntity entity);
    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">
    /// Идентификатор сущности для удаления
    /// </param>
    /// <returns>
    /// Удалил или нет
    /// </returns>
    public Task<bool> DeleteAsync(int id);
}