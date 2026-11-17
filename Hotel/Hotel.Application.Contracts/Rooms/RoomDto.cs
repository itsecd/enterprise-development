namespace Hotel.Application.Contracts.Rooms;

/// <summary>
/// DTO для получения данных номера 
/// </summary>
/// <param name="Id">Идентификатор номера</param>
/// <param name="RoomNumber">Номер комнаты</param>
/// <param name="Floor">Этаж</param>
/// <param name="HasBalcony">Наличие балкона</param>
/// <param name="RoomTypeId">Идентификатор типа номера</param>
public record RoomDto(
    int Id,
    int RoomNumber,
    int Floor,
    bool HasBalcony,
    int RoomTypeId
) : IHasId<int>;
