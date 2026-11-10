namespace Museum.Domain.Interfaces;

/// <summary>
/// Описывает основные операции для работы с данными
/// </summary>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public Task<IReadOnlyList<TEntity>> GetAllAsync();

    /// <summary>
    /// Находит запись по идентификатору
    /// </summary>
    public Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет новую запись
    /// </summary>
    public Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Обновляет существующую запись
    /// </summary>
    public Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}
