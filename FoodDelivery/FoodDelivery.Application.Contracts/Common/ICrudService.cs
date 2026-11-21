namespace FoodDelivery.Application.Contracts.Common;

/// <summary>
/// Определяет основные операции с ресурсом приложения
/// </summary>
/// <typeparam name="TDto">Тип данных, возвращаемых приложением </typeparam>
/// <typeparam name="TRequest">Тип данных для создания и изменения ресурса </typeparam>
public interface ICrudService<TDto, TRequest>
    where TDto : class
    where TRequest : class
{
    /// <summary>
    /// Возвращает все ресурсы
    /// </summary>
    /// <returns>Коллекция ресурсов </returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает ресурс по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ресурса </param>
    /// <returns>Найденный ресурс </returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<TDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт ресурс
    /// </summary>
    /// <param name="request">Данные нового ресурса </param>
    /// <returns>Созданный ресурс с назначенным идентификатором </returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<TDto> CreateAsync(TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Изменяет существующий ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса </param>
    /// <param name="request">Новые данные ресурса </param>
    /// <returns>Изменённый ресурс </returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<TDto> UpdateAsync(int id, TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет ресурс, если он существует 
    /// </summary>
    /// <param name="id">Идентификатор ресурса </param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}