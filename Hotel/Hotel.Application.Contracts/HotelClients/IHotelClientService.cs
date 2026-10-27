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
    public IReadOnlyList<BookingDto>? GetBookingsByClientId(int clientId);

    /// <summary>
    /// Получить топ пять клиентов по общей стоимости проживания
    /// </summary>
    public IReadOnlyList<HotelClientStayCostDto> GetTopFiveClientsByTotalStayCost();

    /// <summary>
    /// Получить клиентов, бронировавших номера указанной категории
    /// </summary>
    public IReadOnlyList<HotelClientDto> GetClientsByRoomCategory(RoomCategory category);
}
