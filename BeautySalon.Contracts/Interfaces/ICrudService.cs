namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Общие операции с ресурсом
/// </summary>
public interface ICrudService<TRequest, TResponse>
{
    /// <summary>
    /// Получить список объектов
    /// </summary>
    Task<IReadOnlyList<TResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<TResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать объект
    /// </summary>
    /// <param name="request">Данные объекта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<TResponse> CreateAsync(TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="request">Новые данные объекта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task UpdateAsync(int id, TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
