using Hotel.Application.Contracts.Bookings;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts.HotelClients;

/// <summary>
/// Интерфейс для работы с клиентами гостиницы
/// </summary>
public interface IHotelClientService
    : ICrudService<HotelClientDto, CreateHotelClientDto, int>
{
    /// <summary>
    /// Получить бронирования указанного клиента
    /// </summary>
    public Task<IReadOnlyList<BookingDto>?> GetBookingsByClientId(int clientId);

    /// <summary>
    /// Получить топ пять клиентов по общей стоимости проживания
    /// </summary>
    public Task<IReadOnlyList<HotelClientStayCostDto>> GetTopFiveClientsByTotalStayCost();

    /// <summary>
    /// Получить клиентов, бронировавших номера указанной категории
    /// </summary>
    public Task<IReadOnlyList<HotelClientDto>> GetClientsByRoomCategory(RoomCategory category);
}
