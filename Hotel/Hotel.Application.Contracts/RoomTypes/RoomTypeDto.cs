using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts.RoomTypes;

/// <summary>
/// DTO для получения данных типа номера
/// </summary>
/// <param name="Id">Идентификатор типа номера</param>
/// <param name="Category">Категория номера</param>
/// <param name="Area">Площадь номера</param>
/// <param name="BedCount">Количество кроватей</param>
/// <param name="HasBathroom">Наличие санузла</param>
/// <param name="PricePerDay">Стоимость проживания за сутки</param>
public record RoomTypeDto(
    int Id,
    RoomCategory Category,
    double Area,
    int BedCount,
    bool HasBathroom,
    decimal PricePerDay
) : IHasId<int>;
