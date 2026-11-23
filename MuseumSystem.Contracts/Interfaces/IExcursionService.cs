using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис для управления экскурсиями
/// </summary>
public interface IExcursionService
{
    /// <summary>
    /// Получить список всех экскурсий
    /// </summary>
    public Task<IEnumerable<ExcursionResponse>> GetAllAsync();

    /// <summary>
    /// Получить экскурсию по идентификатору
    /// </summary>
    public Task<ExcursionResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новую экскурсию
    /// </summary>
    public Task<ExcursionResponse> CreateAsync(ExcursionCreateRequest request);

    /// <summary>
    /// Обновить экскурсию
    /// </summary>
    public Task<bool> UpdateAsync(int id, ExcursionUpdateRequest request);

    /// <summary>
    /// Удалить экскурсию
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}