namespace Hotel.Application.Contracts.Rooms;

/// <summary>
/// DTO для статистики бронирований номеров
/// </summary>
public record RoomBookingCountDto(
    RoomDto Room,
    int BookingCount
);
