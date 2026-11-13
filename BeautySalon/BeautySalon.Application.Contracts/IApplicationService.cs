namespace BeautySalon.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для CRUD операций с Dto
/// </summary>
/// <typeparam name="TDto">Тип Dto для get запросов к данным</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип Dto для put,post запросам к данным</typeparam>
/// <typeparam name="TKey">Тип идентификатора Dto</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
{
    /// <summary>
    /// Создание Dto
    /// </summary>
    /// <param name="dto">Dto</param>
    /// <returns>Dto</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Обновление Dto
    /// </summary>
    /// <param name="dto">Dto</param>
    /// <param name="id">Идентификатор Dto</param>
    /// <returns>Dto</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey id);

    /// <summary>
    /// Удаление Dto
    /// </summary>
    /// <param name="id">Идентификатор Dto</param>
    /// <returns>Результат удаления</returns>
    public Task<bool> Delete(TKey id);

    /// <summary>
    /// Получение Dto
    /// </summary>
    /// <param name="id">Идентификатор Dto</param>
    /// <returns>Dto</returns>
    public Task<TDto?> Get(TKey id);

    /// <summary>
    /// Получение всех Dto
    /// </summary>
    /// <returns>Список всех Dto</returns>
    public Task<IList<TDto>> GetAll();
}
