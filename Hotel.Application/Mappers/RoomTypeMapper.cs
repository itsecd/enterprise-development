using Hotel.Application.Contracts.RoomType;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappers;

/// <summary>
/// Маппер для перевода типов номеров в DTO и наоборот
/// </summary>
public static class RoomTypeMapper
{
    public static RoomTypeResponseDto ToDto(RoomType t) => new(
        t.Id,
        t.Area,
        t.Beds,
        t.HasBath,
        t.HasShower,
        t.DayPrice,
        t.Category
    );
    public static RoomType ToEntity(RoomTypeCreateUpdateDto dto) => new()
    {
        Area = dto.Area!.Value,
        Beds = dto.Beds!.Value,
        HasBath = dto.HasBath!.Value,
        HasShower = dto.HasShower!.Value,
        DayPrice = dto.DayPrice!.Value,
        Category = dto.Category!.Value
    };
}