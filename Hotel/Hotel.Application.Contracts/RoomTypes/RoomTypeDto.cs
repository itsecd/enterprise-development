using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts.RoomTypes;

/// <summary>
/// DTO для получения данных типа номера
/// </summary>
public record RoomTypeDto(
    int Id,
    RoomCategory Category,
    double Area,
    int BedCount,
    bool HasBathroom,
    decimal PricePerDay
) : IHasId<int>;
