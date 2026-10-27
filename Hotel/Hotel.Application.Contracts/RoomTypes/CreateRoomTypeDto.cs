using Hotel.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.Contracts.RoomTypes;

/// <summary>
/// DTO для создания типа номера
/// </summary>
public record CreateRoomTypeDto(
    [EnumDataType(typeof(RoomCategory), ErrorMessage = "Указана неизвестная категория номера")]
    RoomCategory Category,

    [Range(0.1, 10000.0, ErrorMessage = "Площадь должна быть от 0,1 до 10000")]
    double Area,

    [Range(1, 100, ErrorMessage = "Количество кроватей должно быть от 1 до 100")]
    int BedCount,

    bool HasBathroom,

    [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "Цена за день должна быть от 0,01 до 1000000")]
    decimal PricePerDay
);
