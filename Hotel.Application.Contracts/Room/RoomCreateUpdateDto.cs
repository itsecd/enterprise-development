namespace Hotel.Application.Contracts.Room;
/// <summary>
/// DTO для PUT/POST запросов к номерам
/// </summary>
/// <param name="Number">Номер комнаты</param>
/// <param name="Floor">Этаж номера</param>
/// <param name="HasBalcony">Наличие балкона в номере</param>
/// <param name="RoomTypeId">Id типа номера</param>
public record RoomCreateUpdateDto(
    uint? Number,
    uint? Floor,
    bool? HasBalcony,
    int? RoomTypeId
);