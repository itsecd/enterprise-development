namespace AutoService.Application.Interfaces;

/// <summary>Операции над входными и выходными контрактами ресурса.</summary>
/// <typeparam name="TRequest">Данные записи.</typeparam>
/// <typeparam name="TDto">Представление результата.</typeparam>
public interface ICrudService<TRequest, TDto>
{
    /// <summary>Возвращает все записи.</summary>
    public Task<IReadOnlyList<TDto>> GetAllAsync();
    /// <summary>Возвращает запись по идентификатору.</summary>
    public Task<TDto> GetAsync(int id);
    /// <summary>Создаёт запись.</summary>
    public Task<TDto> CreateAsync(TRequest request);
    /// <summary>Полностью обновляет существующую запись.</summary>
    public Task UpdateAsync(int id, TRequest request);
    /// <summary>Удаляет запись, если она не используется.</summary>
    public Task DeleteAsync(int id);
}
