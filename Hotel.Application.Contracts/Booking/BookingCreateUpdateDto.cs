namespace Hotel.Application.Contracts.Booking;
/// <summary>
/// DTO для PUT/POST запросов к бронированиям
/// </summary>
/// <param name="ClientId">Id клиента</param>
/// <param name="RoomId">Id номера</param>
/// <param name="CheckInDate">Дата заезда</param>
/// <param name="DaysOfStay">Дата отъезда</param>
public record BookingCreateUpdateDto(
    int? ClientId,
    int? RoomId,
    DateOnly? CheckInDate,
    uint? DaysOfStay
);