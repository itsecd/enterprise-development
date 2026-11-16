namespace Hotel.Application.Contracts.Rooms;

/// <summary>
/// DTO для статистики бронирований номеров
/// </summary>
/// <param name="Room">Данные номера</param>
/// <param name="BookingCount">Количество бронирований</param>
public record RoomBookingCountDto(
    RoomDto Room,
    int BookingCount
);
