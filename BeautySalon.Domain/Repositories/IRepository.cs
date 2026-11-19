using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Общие операции с сущностями
/// </summary>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Получить все объекты
    /// </summary>
    IReadOnlyList<TEntity> GetAll();

    /// <summary>
    /// Найти объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Найденный объект или null</returns>
    TEntity? GetById(int id);

    /// <summary>
    /// Добавить объект и назначить ему идентификатор
    /// </summary>
    /// <param name="entity">Добавляемый объект</param>
    /// <returns>Сохранённый объект</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    /// <param name="entity">Новые данные с идентификатором объекта</param>
    /// <returns>Признак успешного обновления</returns>
    bool Update(TEntity entity);

    /// <summary>
    /// Удалить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Признак успешного удаления</returns>
    bool Delete(int id);
}
