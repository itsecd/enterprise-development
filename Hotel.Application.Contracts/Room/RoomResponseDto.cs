using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts.Room;

/// <summary>
/// DTO для GET запросов к номерам
/// </summary>
/// <param name="Id">Id номера</param>
/// <param name="Number">номер комнаты</param>
/// <param name="Floor">Этаж номера</param>
/// <param name="HasBalcony">Наличие балкона в номере</param>
/// <param name="RoomTypeId">Id типа номера</param>
/// <param name="RoomTypeName">Имя типа номера</param>
/// <param name="DayPrice">Посуточная цена бронирования</param>
public record RoomResponseDto(
    int Id,
    uint Number,
    uint Floor,
    bool HasBalcony,
    int RoomTypeId,
    RoomCategory RoomTypeName,
    double DayPrice
);