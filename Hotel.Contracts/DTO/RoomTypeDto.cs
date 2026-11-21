using Hotel.Domain.Enums;

namespace Hotel.Contracts.DTO;

/// <summary>
/// Dto для передачи информации о типе комнаты
/// </summary>
/// <param name="Id">Id типа комнаты</param>
/// <param name="Category">Категория номера</param>
/// <param name="RoomArea">Площадь комнаты</param>
/// <param name="BedCount">Количество кроватей</param>
/// <param name="HasBathroom">Наличие ваннной комнаты</param>
/// <param name="Price">Цена за сутки</param>
public sealed record RoomTypeDto(
    int Id,
    RoomCategory Category,
    int RoomArea,
    int BedCount,
    bool HasBathroom,
    decimal Price);