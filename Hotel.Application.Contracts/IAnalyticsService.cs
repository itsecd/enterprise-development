using Hotel.Application.Contracts.Booking;
using Hotel.Application.Contracts.Client;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts;

/// <summary>
/// DTO категории номеров и их клиентов
/// </summary>
/// <param name="Category">Категория номеров</param>
/// <param name="Clients">Клиенты</param>
public record ClientsByRoomTypeDto(
    RoomCategory Category,
    IEnumerable<ClientResponseDto> Clients
);

/// <summary>
/// DTO номера и кол-ва его бронирований
/// </summary>
/// <param name="RoomId">Id номера</param>
/// <param name="RoomNumber">Номер комнаты</param>
/// <param name="BookingCount">Кол-во бронирований</param>
public record RoomBookingCountDto(
    int RoomId,
    uint RoomNumber,
    int BookingCount
);

/// <summary>
/// DTO клиента и его суммарных трат на бронирования
/// </summary>
/// <param name="Client">Клиент</param>
/// <param name="TotalSpent">Сумма бронирований</param>
public record ClientTotalSpentDto(
    ClientResponseDto Client,
    double TotalSpent
);

/// <summary>
/// Интерфейс служды аналитической службы
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить всех клиентов по категории номера
    /// </summary>
    /// <param name="category">категория номера</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns></returns>
    public Task<ClientsByRoomTypeDto> GetClientsByRoomTypeAsync(RoomCategory category, CancellationToken ct = default);
    
    /// <summary>
    /// Получение текущих бронирований
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Бронирования</returns>
    public Task<IEnumerable<BookingResponseDto>> GetCurrentBookingsAsync(CancellationToken ct = default);
    
    /// <summary>
    /// 5 самых бронируемых номеров
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>5 номеров</returns>
    public Task<IEnumerable<RoomBookingCountDto>> GetTop5FrequentlyBookedRoomsAsync(CancellationToken ct = default);
    
    /// <summary>
    /// Получить номера и кол-во их броинрованя
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Номера и кол-во броинрований</returns>
    public Task<IEnumerable<RoomBookingCountDto>> GetBookingCountForEachRoomAsync(CancellationToken ct = default);
    
    /// <summary>
    /// 5 самых дорогих клиентов
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>5 клиентов и их сумма трат</returns>
    public Task<IEnumerable<ClientTotalSpentDto>> GetTop5ClientsByTotalSpentAsync(CancellationToken ct = default);
}