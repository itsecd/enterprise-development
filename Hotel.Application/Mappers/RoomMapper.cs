using Hotel.Application.Contracts.Room;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappers;

/// <summary>
/// Маппер для перевода номеров в DTO и наоборот
/// </summary>
public static class RoomMapper
{
    public static RoomResponseDto ToDto(Room r) => new(
        r.Id,
        r.Number,
        r.Floor,
        r.HasBalcony,
        RoomTypeMapper.ToDto(r.Type)
    );
    public static Room ToEntity(RoomCreateUpdateDto dto, RoomType type) => new()
    {
        Floor = dto.Floor!.Value,
        HasBalcony = dto.HasBalcony!.Value,
        Number = dto!.Number!.Value,
        Type = type
    };
}
