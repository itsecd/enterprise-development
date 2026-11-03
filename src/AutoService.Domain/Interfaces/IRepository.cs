using System.Linq.Expressions;

namespace AutoService.Domain.Interfaces;

/// <summary>Операции над коллекцией сущностей в рамках сеанса хранилища.</summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>Возвращает записи в порядке идентификаторов.</summary>
    public Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>Возвращает запись или null.</summary>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>Отбирает записи по набору идентификаторов.</summary>
    public Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyCollection<int> ids);

    /// <summary>Отбирает записи на стороне хранилища.</summary>
    public Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>Проверяет наличие подходящей записи без создания списка.</summary>
    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    /// <summary>Добавляет сущность и назначает новый идентификатор.</summary>
    public Task<T> AddAsync(T entity);

    /// <summary>Обновляет существующий объект, сохраняя ссылки на него.</summary>
    public Task UpdateAsync(int id, Action<T> update);

    /// <summary>Удаляет существующую запись.</summary>
    public Task DeleteAsync(int id);
}
