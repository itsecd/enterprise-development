namespace Carwash.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для CRUD операций с DTO
/// </summary>
/// <typeparam name="TDto">Тип DTO для получения данных</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания и обновления</typeparam>
/// <typeparam name="TKey">Тип идентификатора DTO</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
{
    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>DTO</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Обновление DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="id">Идентификатор DTO</param>
    /// <returns>DTO</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey id);

    /// <summary>
    /// Удаление DTO
    /// </summary>
    /// <param name="id">Идентификатор DTO</param>
    /// <returns>True - если удаление успешно, иначе False</returns>
    public Task<bool> Delete(TKey id);

    /// <summary>
    /// Получение DTO
    /// </summary>
    /// <param name="id">Идентификатор DTO</param>
    /// <returns>DTO</returns>
    public Task<TDto?> Read(TKey id);

    /// <summary>
    /// Получение всех DTO
    /// </summary>
    /// <returns>Список DTO</returns>
    public Task<IList<TDto>> ReadAll();
}
