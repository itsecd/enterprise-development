using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис для управления посетителями
/// </summary>
public interface IVisitorService
{
    /// <summary>
    /// Получить список всех посетителей
    /// </summary>
    public Task<IEnumerable<VisitorResponse>> GetAllAsync();

    /// <summary>
    /// Получить посетителя по идентификатору
    /// </summary>
    public Task<VisitorResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создать нового посетителя
    /// </summary>
    public Task<VisitorResponse> CreateAsync(VisitorCreateRequest request);

    /// <summary>
    /// Обновить данные посетителя
    /// </summary>
    public Task<bool> UpdateAsync(int id, VisitorUpdateRequest request);

    /// <summary>
    /// Удалить посетителя
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}