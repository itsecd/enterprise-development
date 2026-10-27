namespace Hotel.Application.Contracts.Rooms;

/// <summary>
/// DTO для получения данных номера 
/// </summary>
public record RoomDto(
    int Id,
    int RoomNumber,
    int Floor,
    bool HasBalcony,
    int RoomTypeId
) : IHasId<int>;
