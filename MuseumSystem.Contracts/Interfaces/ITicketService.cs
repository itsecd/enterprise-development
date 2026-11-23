using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис для управления билетами
/// </summary>
public interface ITicketService
{
    /// <summary>
    /// Получить список всех билетов
    /// </summary>
    public Task<IEnumerable<TicketResponse>> GetAllAsync();

    /// <summary>
    /// Получить билет по идентификатору
    /// </summary>
    public Task<TicketResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новый билет
    /// </summary>
    public Task<TicketResponse> CreateAsync(TicketCreateRequest request);

    /// <summary>
    /// Обновить данные билета
    /// </summary>
    public Task<bool> UpdateAsync(int id, TicketUpdateRequest request);

    /// <summary>
    /// Удалить билет
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}