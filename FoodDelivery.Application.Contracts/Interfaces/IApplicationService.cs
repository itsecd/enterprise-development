namespace FoodDelivery.Application.Contracts.Interfaces;

/// <summary>
/// Определяет стандартные асинхронные операции CRUD для прикладных сервисов
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
{
    /// <summary>
    /// Асинхронно создаёт новую запись
    /// </summary>

    Task<TDto> CreateAsync(TCreateUpdateDto dto);

    /// <summary>
    /// Асинхронно возвращает запись по идентификатору
    /// </summary>
    Task<TDto?> GetAsync(TKey id);

    /// <summary>
    /// Асинхронно возвращает все записи
    /// </summary>
    Task<IList<TDto>> GetAllAsync();

    /// <summary>
    /// Асинхронно обновляет существующую запись
    /// </summary>
    Task<TDto> UpdateAsync(TCreateUpdateDto dto, TKey id);

    /// <summary>
    /// Асинхронно удаляет запись
    /// </summary>
    Task<bool> DeleteAsync(TKey id);
}