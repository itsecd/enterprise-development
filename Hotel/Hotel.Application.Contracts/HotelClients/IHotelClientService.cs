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
    /// <param name="clientId">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<BookingDto>?> GetBookingsByClientId(int clientId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить топ пять клиентов по общей стоимости проживания
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<HotelClientStayCostDto>> GetTopFiveClientsByTotalStayCost(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить клиентов, бронировавших номера указанной категории
    /// </summary>
    /// <param name="category">Категория номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<HotelClientDto>> GetClientsByRoomCategory(RoomCategory category, CancellationToken cancellationToken = default);
}
