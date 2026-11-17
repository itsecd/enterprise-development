namespace Hotel.Application.Contracts.Bookings;

/// <summary>
/// DTO для получения данных бронирования
/// </summary>
/// <param name="Id">Идентификатор бронирования</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="RoomId">Идентификатор номера</param>
/// <param name="CheckInDate">Дата заезда</param>
/// <param name="DaysCount">Количество дней проживания</param>
public record BookingDto(
    int Id,
    int ClientId,
    int RoomId,
    DateOnly CheckInDate,
    int DaysCount
) : IHasId<int>;
