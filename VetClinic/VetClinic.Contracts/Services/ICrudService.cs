namespace VetClinic.Contracts.Services;

/// <summary>
/// Базовые CRUD-операции над сущностью через DTO
/// </summary>
/// <typeparam name="TDto">DTO для чтения</typeparam>
/// <typeparam name="TEditDto">DTO для создания и изменения</typeparam>
public interface ICrudService<TDto, TEditDto>
    where TDto : class
    where TEditDto : class
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность или null, если она не найдена</returns>
    public Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать сущность
    /// </summary>
    /// <param name="dto">Данные новой сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданная сущность с присвоенным идентификатором</returns>
    /// <exception cref="KeyNotFoundException">Связанная сущность не найдена</exception>
    public Task<TDto> CreateAsync(TEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Изменить сущность
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="dto">Новые данные сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Изменённая сущность или null, если она не найдена</returns>
    /// <exception cref="KeyNotFoundException">Связанная сущность не найдена</exception>
    public Task<TDto?> UpdateAsync(int id, TEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить сущность, повторное удаление не считается ошибкой
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
