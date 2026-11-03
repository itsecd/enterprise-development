using HotelSystem.Application.Contracts.Bookings;
using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Contracts.Rooms;
using HotelSystem.Application.Contracts.RoomTypes;
using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Mapping;

/// <summary>
/// Методы преобразования между сущностями домена и контрактами API
/// </summary>
internal static class MappingExtensions
{
    /// <summary>
    /// Преобразует клиента в контракт ответа
    /// </summary>
    public static ClientResponse ToResponse(this Client client) => new()
    {
        Id = client.Id,
        PassportNumber = client.PassportNumber,
        LastName = client.LastName,
        FirstName = client.FirstName,
        Patronymic = client.Patronymic,
        BirthDate = client.BirthDate,
        Citizenship = client.Citizenship
    };

    /// <summary>
    /// Преобразует контракт запроса в клиента
    /// </summary>
    public static Client ToEntity(this ClientRequest request) => new()
    {
        PassportNumber = request.PassportNumber,
        LastName = request.LastName,
        FirstName = request.FirstName,
        Patronymic = request.Patronymic,
        BirthDate = request.BirthDate,
        Citizenship = request.Citizenship
    };

    /// <summary>
    /// Преобразует тип номера в контракт ответа
    /// </summary>
    public static RoomTypeResponse ToResponse(this RoomType roomType) => new()
    {
        Id = roomType.Id,
        Category = roomType.Category,
        Area = roomType.Area,
        BedCount = roomType.BedCount,
        HasBathOrShower = roomType.HasBathOrShower,
        PricePerDay = roomType.PricePerDay
    };

    /// <summary>
    /// Преобразует контракт запроса в тип номера
    /// </summary>
    public static RoomType ToEntity(this RoomTypeRequest request) => new()
    {
        Category = request.Category,
        Area = request.Area,
        BedCount = request.BedCount,
        HasBathOrShower = request.HasBathOrShower,
        PricePerDay = request.PricePerDay
    };

    /// <summary>
    /// Преобразует номер в контракт ответа
    /// </summary>
    public static RoomResponse ToResponse(this Room room) => new()
    {
        Id = room.Id,
        RoomNumber = room.RoomNumber,
        Floor = room.Floor,
        HasBalcony = room.HasBalcony,
        RoomTypeId = room.RoomType.Id,
        RoomCategory = room.RoomType.Category.ToString()
    };

    /// <summary>
    /// Преобразует контракт запроса в номер
    /// </summary>
    public static Room ToEntity(this RoomRequest request, RoomType roomType) => new()
    {
        RoomNumber = request.RoomNumber,
        Floor = request.Floor,
        HasBalcony = request.HasBalcony,
        RoomType = roomType
    };

    /// <summary>
    /// Преобразует бронирование в контракт ответа
    /// </summary>
    public static BookingResponse ToResponse(this Booking booking) => new()
    {
        Id = booking.Id,
        ClientId = booking.Client.Id,
        RoomId = booking.Room.Id,
        CheckInDate = booking.CheckInDate,
        NumberOfDays = booking.NumberOfDays,
        TotalCost = booking.TotalCost
    };

    /// <summary>
    /// Преобразует контракт запроса в бронирование
    /// </summary>
    public static Booking ToEntity(
        this BookingRequest request,
        Client client,
        Room room) => new()
    {
        Client = client,
        Room = room,
        CheckInDate = request.CheckInDate,
        NumberOfDays = request.NumberOfDays
    };
}