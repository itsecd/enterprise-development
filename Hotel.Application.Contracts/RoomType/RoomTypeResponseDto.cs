using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts.RoomType;

/// <summary>
/// DTO для GET запросов к типам номеров
/// </summary>
/// <param name="Id">Id типа</param>
/// <param name="Area">Площадь номера</param>
/// <param name="Beds">Кол-во кроватей</param>
/// <param name="HasBath">Наличие ванны</param>
/// <param name="HasShower">Наличие душа</param>
/// <param name="DayPrice">Посуточная цена</param>
/// <param name="Category">Категория комнаты</param>
public record RoomTypeResponseDto(
    int Id,
    double Area,
    uint Beds,
    bool HasBath,
    bool HasShower,
    double DayPrice,
    RoomCategory Category
);