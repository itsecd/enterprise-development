using Hotel.Contracts.DTO;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mapping;

/// <summary>
/// Преобразует сущности в Dto
/// </summary>
public static class DtoMapping
{
    /// <summary>
    /// Преобразует сущности(клиенты) в Dto
    /// </summary>
    /// <param name="entity">сущность</param>
    public static ClientDto ToDto(this Client entity) => new(
        entity.Id,
        entity.LastName,
        entity.FirstName,
        entity.Patronymic,
        entity.BirthDate,
        entity.NumberPassport,
        entity.Citizenship);

    /// <summary>
    /// Преобразует сущности(Типы комнат) в Dto
    /// </summary>
    /// <param name="entity">сущность</param>
    public static RoomTypeDto ToDto(this RoomType entity) => new(
        entity.Id,
        entity.Category,
        entity.RoomArea,
        entity.BedCount,
        entity.HasBathroom,
        entity.Price);

    /// <summary>
    /// Преобразует сущности(комнаты) в Dto
    /// </summary>
    /// <param name="entity">сущность</param>
    public static RoomDto ToDto(this Room entity) => new(
        entity.Id,
        entity.Number,
        entity.Floor,
        entity.HasBalcony,
        entity.RoomType.ToDto());

    /// <summary>
    /// Преобразует сущности(бронирования) в Dto
    /// </summary>
    /// <param name="entity">сущность</param>
    public static BookingDto ToDto(this Booking entity) => new(
        entity.Id,
        entity.DateArrival,
        entity.DayCount,
        entity.Client.ToDto(),
        entity.Room.ToDto());
}