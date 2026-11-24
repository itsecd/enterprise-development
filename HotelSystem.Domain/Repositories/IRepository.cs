namespace HotelSystem.Domain.Repositories;

/// <summary>
/// Порт для асинхронного доступа к хранилищу сущностей
/// </summary>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Асинхронно возвращает все сущности
    /// </summary>
    public Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Асинхронно возвращает сущность по идентификатору
    /// </summary>
    public Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Асинхронно добавляет новую сущность
    /// </summary>
    public Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Асинхронно заменяет существующую сущность
    /// </summary>
    public Task<bool> UpdateAsync(int id, TEntity entity);

    /// <summary>
    /// Асинхронно удаляет сущность по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}