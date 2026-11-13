namespace FoodDelivery.Application.Interfaces;

/// <summary>
/// Определяет базовые операции доступа к данным
/// </summary>
public interface IRepository<T>
    where T : FoodDelivery.Domain.Entities.IEntity
{
    /// <summary>
    /// Возвращает список всех сущностей
    /// </summary>
    Task<IList<T>> GetAllAsync();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет новую сущность
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    Task<T?> UpdateAsync(T entity);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    Task<bool> DeleteAsync(int id);
}