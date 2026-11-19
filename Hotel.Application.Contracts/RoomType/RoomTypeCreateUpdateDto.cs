namespace Hotel.Application.Contracts.RoomType;

using Hotel.Domain.Shared.Enums;

/// <summary>
/// DTO для PUT/POST запросов к типам номеров
/// </summary>
/// <param name="Area">Площадь номера</param>
/// <param name="Beds">Кол-во кроватей</param>
/// <param name="HasBath">Наличие Ванны</param>
/// <param name="HasShower">Наличие душа</param>
/// <param name="DayPrice">Посуточная цена бронирования</param>
/// <param name="Category">Категория номера</param>
public record RoomTypeCreateUpdateDto(
    double? Area,
    uint? Beds,
    bool? HasBath,
    bool? HasShower,
    double? DayPrice,
    RoomCategory? Category
);