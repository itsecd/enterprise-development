
namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Общий интерфейс репозитория для работы с сущностями.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получить все сущности.
    /// </summary>
    IReadOnlyList<T> GetAll();

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    T? GetById(int id);

    /// <summary>
    /// Добавить новую сущность.
    /// </summary>
    T Add(T entity);

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    bool Update(int id, T entity);

    /// <summary>
    /// Удалить сущность по идентификатору.
    /// </summary>
    bool Delete(int id);
}
