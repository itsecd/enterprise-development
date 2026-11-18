namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет операции хранения доменных сущностей
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
public interface IRepository<TEntity> : IDataSession
    where TEntity : class
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    IReadOnlyList<TEntity> GetAll();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Сущность или null, если она не найдена</returns>
    TEntity? GetById(int id);

    /// <summary>
    /// Добавляет сущность и назначает ей идентификатор
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <returns>Добавленная сущность</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    /// <param name="entity">Сущность с новыми значениями свойств</param>
    /// <returns>Признак успешного обновления</returns>
    bool Update(TEntity entity);

    /// <summary>
    /// Удаляет сущность, если она существует
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    void Delete(int id);
}
