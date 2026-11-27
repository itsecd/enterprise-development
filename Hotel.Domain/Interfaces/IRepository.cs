using System.Linq.Expressions;

namespace Hotel.Domain.Interfaces;

/// <summary>
/// Интерфейс для работы с хранилищем сущностей
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    /// <returns></returns>
    public Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Вернуть сущность
    /// </summary>
    /// <param name="id">Id сущности</param>
    /// <returns>Сущность</returns>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавление сущностей
    /// </summary>
    /// <param name="entity">сущность</param>
    /// <returns></returns>
    public Task<T> AddAsync(T entity);

    /// <summary>
    /// Изменить параметры сущности
    /// </summary>
    /// <param name="id">Id обновляемой сущности</param>
    /// <param name="update"></param>
    /// <returns></returns>
    public Task<bool> UpdateAsync(int id, Action<T> update);

    /// <summary>
    /// Удаление сущности 
    /// </summary>
    /// <param name="id">Id удаляемой сущности</param>
    /// <returns></returns>
    public Task DeleteAsync(int id);

    /// <summary>
    /// Получение сущностей по условию
    /// </summary>
    /// <param name="predicate">Условие фильтрации</param>
    /// <returns>Список найденных сущностей</returns>
    public Task<IReadOnlyList<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
}