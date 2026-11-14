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
    public Task<IReadOnlyList<RoomBookingCountDto>> GetTopFiveMostBookedRooms();

    /// <summary>
    /// Получить количество бронирований для каждого номера
    /// </summary>
    public Task<IReadOnlyList<RoomBookingCountDto>> GetBookingCountForEachRoom();

    /// <summary>
    /// Получить бронирования указанного номера
    /// </summary>
    public Task<IReadOnlyList<BookingDto>?> GetBookingsByRoomId(int roomId);
}
