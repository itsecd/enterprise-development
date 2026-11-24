using HotelSystem.Application.Contracts;

namespace HotelSystem.Application.Services;

/// <summary>
/// Определяет общие CRUD-операции для сервисов приложения
/// </summary>
public interface ICrudService<TRequest, TResponse>
    where TResponse : IEntityResponse
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    public Task<List<TResponse>> GetAllAsync();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public Task<TResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт сущность
    /// </summary>
    public Task<TResponse> AddAsync(TRequest request);

    /// <summary>
    /// Обновляет сущность
    /// </summary>
    public Task<bool> UpdateAsync(int id, TRequest request);

    /// <summary>
    /// Удаляет сущность
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}