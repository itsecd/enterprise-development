using Hotel.Application.Contracts.Client;
using Hotel.Application.Contracts.Room;

namespace Hotel.Application.Contracts.Booking;

/// <summary>
/// DTO для GET запросов к бронирования
/// </summary>
/// <param name="Id">Id брони</param>
/// <param name="Client">DTO с информацией клиента</param>
/// <param name="Room">DTO с информацией комнаты</param>
/// <param name="CheckInDate">Дата заезда</param>
/// <param name="DaysOfStay">Дата отъезда</param>
/// <param name="TotalPrice">Стоимость бронирования</param>
public record BookingResponseDto(
    int Id,
    ClientResponseDto Client,
    RoomResponseDto Room,
    DateOnly CheckInDate,
    uint DaysOfStay,
    double TotalPrice
);