namespace CarWash.Contracts.Services;

/// <summary>
/// Базовые операции приложения с сущностью
/// </summary>
/// <typeparam name="TRequest">Данные для создания и изменения</typeparam>
/// <typeparam name="TResponse">Представление сущности</typeparam>
public interface ICrudService<TRequest, TResponse>
{
    /// <summary>
    /// Получить список сущностей
    /// </summary>
    Task<IReadOnlyList<TResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    Task<TResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать сущность
    /// </summary>
    Task<TResponse> CreateAsync(TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Изменить сущность
    /// </summary>
    Task UpdateAsync(int id, TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
