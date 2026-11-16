using Hotel.Application.Contracts.Bookings;

namespace Hotel.Application.Contracts.Rooms;

/// <summary>
/// Интерфейс для работы с номерами гостиницы
/// </summary>
public interface IRoomService
    : ICrudService<RoomDto, CreateRoomDto, int>
{
    /// <summary>
    /// Получить пять номеров с наибольшим количеством бронирований
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<RoomBookingCountDto>> GetTopFiveMostBookedRooms(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить количество бронирований для каждого номера
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<RoomBookingCountDto>> GetBookingCountForEachRoom(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить бронирования указанного номера
    /// </summary>
    /// <param name="roomId">Идентификатор номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<BookingDto>?> GetBookingsByRoomId(int roomId, CancellationToken cancellationToken = default);
}
