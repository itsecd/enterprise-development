namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Базовый generic-интерфейс репозитория для работы с сущностями
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получить список всех сущностей
    /// </summary>
    public Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавить новую сущность
    /// </summary>
    public Task<T> AddAsync(T entity);

    /// <summary>
    /// Обновить данные сущности
    /// </summary>
    public Task<bool> UpdateAsync(T entity);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}