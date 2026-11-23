using Hotel.Application.Contracts.Booking;
using Hotel.Application.Mappers;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappers;

/// <summary>
/// Маппер для перевода бронирований в DTO и наоборот
/// </summary>
public static class BookingMapper
{
    public static BookingResponseDto ToDto(Booking b) => new(
        b.Id,
        ClientMapper.ToDto(b.Client),
        RoomMapper.ToDto(b.Room),
        b.CheckInDate,
        b.DaysOfStay,
        b.TotalPrice
    );

    public static Booking ToEntity(BookingCreateUpdateDto dto, Client client, Room room) => new()
    {
        Client = client,
        Room = room,
        CheckInDate = dto.CheckInDate!.Value,
        DaysOfStay = dto.DaysOfStay!.Value
    };
}