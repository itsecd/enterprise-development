namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Общий контракт CRUD-операций для ресурсов API
/// </summary>
/// <typeparam name="TDto">
/// DTO, возвращаемый клиенту
/// </typeparam>
/// <typeparam name="TEditDto">
/// DTO входных данных для создания и изменения
/// </typeparam>
public interface ICrudService<TDto, TEditDto>
{
    /// <summary>
    /// Получает все ресурсы
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список DTO ресурсов</returns>
    public Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает ресурс по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>DTO найденного ресурса</returns>
    public Task<TDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт ресурс
    /// </summary>
    /// <param name="editDto">Данные для создания</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>DTO созданного ресурса</returns>
    public Task<TDto> AddAsync(TEditDto editDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет ресурс
    /// </summary>
    /// <param name="id">Идентификатор обновляемого ресурса</param>
    /// <param name="editDto">Данные для обновления</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>DTO обновлённого ресурса</returns>
    public Task<TDto> UpdateAsync(int id, TEditDto editDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет ресурс
    /// </summary>
    /// <param name="id">Идентификатор удаляемого ресурса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns><see langword="true"/>, если ресурс найден и удалён; иначе <see langword="false"/></returns>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}