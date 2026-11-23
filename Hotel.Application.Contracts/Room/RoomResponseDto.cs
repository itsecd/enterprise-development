using Hotel.Application.Contracts.RoomType;

namespace Hotel.Application.Contracts.Room;

/// <summary>
/// DTO для GET запросов к номерам
/// </summary>
/// <param name="Id">Id номера</param>
/// <param name="Number">номер комнаты</param>
/// <param name="Floor">Этаж номера</param>
/// <param name="HasBalcony">Наличие балкона в номере</param>
/// <param name="RoomType">DTO с информацией о типе комнаты</param>
public record RoomResponseDto(
    int Id,
    uint Number,
    uint Floor,
    bool HasBalcony,
    RoomTypeResponseDto RoomType
);