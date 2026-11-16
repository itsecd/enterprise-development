namespace CarWash.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для CRUD операций
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создание DTO
    /// </summary>
    Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получение DTO по идентификатору
    /// </summary>
    Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Получение всего списка DTO
    /// </summary>
    Task<IList<TDto>> GetAll();

    /// <summary>
    /// Обновление DTO
    /// </summary>
    Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Удаление DTO
    /// </summary>
    Task<bool> Delete(TKey dtoId);
}