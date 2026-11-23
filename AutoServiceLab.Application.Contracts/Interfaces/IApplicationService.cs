namespace AutoServiceLab.Application.Contracts.Interfaces;

/// <summary>
/// Базовый контракт для CRUD-сервисов
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создать запись
    /// </summary>
    public Task<TDto> CreateAsync(TCreateUpdateDto dto);

    /// <summary>
    /// Получить запись по идентификатору
    /// </summary>
    public Task<TDto?> GetAsync(TKey id);

    /// <summary>
    /// Получить все записи
    /// </summary>
    public Task<IList<TDto>> GetAllAsync();

    /// <summary>
    /// Обновить запись
    /// </summary>
    public Task<TDto> UpdateAsync(TCreateUpdateDto dto, TKey id);

    /// <summary>
    /// Удалить запись
    /// </summary>
    public Task<bool> DeleteAsync(TKey id);
}
