namespace Hotel.Application.Contracts.Booking;
/// <summary>
/// DTO для GET запросов к бронирования
/// </summary>
/// <param name="Id">Id брони</param>
/// <param name="ClientId">Id клиента</param>
/// <param name="FirstName">Имя клиента</param>
/// <param name="LastName">Фамилия клиента</param>
/// <param name="Patronymic">Отчество клиента</param>
/// <param name="RoomId">Id номера</param>
/// <param name="RoomNumber">Номер комнаты</param>
/// <param name="CheckInDate">Дата заезда</param>
/// <param name="DaysOfStay">Дата отъезда</param>
/// <param name="TotalPrice">Стоимость бронирования</param>
public record BookingResponseDto(
    int Id,
    int ClientId,
    string FirstName,
    string LastName,
    string? Patronymic,
    int RoomId,
    uint RoomNumber,
    DateOnly CheckInDate,
    uint DaysOfStay,
    double TotalPrice
);