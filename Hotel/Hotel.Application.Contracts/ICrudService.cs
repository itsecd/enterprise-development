namespace Hotel.Application.Contracts;

/// <summary>
/// Обобщенный CRUD интерфейс
/// </summary>
/// <typeparam name="TDto">DTO для получения данных</typeparam>
/// <typeparam name="TInputDto">DTO для создания и обновления</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface ICrudService<TDto, TInputDto, TKey>
    where TKey : notnull
{
    /// <summary>
    /// Получить список всех объектов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<TDto>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<TDto?> GetById(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать объект
    /// </summary>
    /// <param name="dto">Данные для создания записи</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<TDto> Create(TInputDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="dto">Данные для обновления</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<TDto?> Update(TKey id, TInputDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<bool> Delete(TKey id, CancellationToken cancellationToken = default);
}
