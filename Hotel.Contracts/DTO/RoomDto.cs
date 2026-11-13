namespace Hotel.Contracts.DTO;

/// <summary>
/// Dto для передачи информации о комнате
/// </summary>
/// <param name="Id">Id комнаты</param>
/// <param name="Number">Номер комнаты</param>
/// <param name="Floor">Этаж</param>
/// <param name="HasBalcony">Наличие балкона</param>
/// <param name="RoomType">Тип комнаты</param>
public sealed record RoomDto(
    int Id,
    int Number,
    int Floor,
    bool HasBalcony,
    RoomTypeDto RoomType);