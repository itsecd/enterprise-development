namespace Hotel.Application.Contracts.Bookings;

/// <summary>
/// DTO для получения данных бронирования
/// </summary>
public record BookingDto(
    int Id,
    int ClientId,
    int RoomId,
    DateOnly CheckInDate,
    int DaysCount
) : IHasId<int>;
