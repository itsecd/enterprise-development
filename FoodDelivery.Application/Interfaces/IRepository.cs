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
    IList<T> GetAll();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    T? GetById(int id);

    /// <summary>
    /// Добавляет новую сущность
    /// </summary>
    T Add(T entity);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    T? Update(T entity);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    bool Delete(int id);
}