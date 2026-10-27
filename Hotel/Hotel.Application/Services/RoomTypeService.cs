using Hotel.Application.Contracts.RoomTypes;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

/// <summary>
/// Служба работы с типами номеров
/// </summary>
public class RoomTypeService(IRepository<RoomType, int> repository) : IRoomTypeService
{
    /// <summary>
    /// Получить все типы номеров
    /// </summary>
    public IReadOnlyList<RoomTypeDto> GetAll()
    {
        return repository.GetAll()
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить тип номера по идентификатору
    /// </summary>
    public RoomTypeDto? GetById(int id)
    {
        var roomType = repository.GetById(id);

        return roomType is null ? null : ToDto(roomType);
    }

    /// <summary>
    /// Создать тип номера
    /// </summary>
    public RoomTypeDto Create(CreateRoomTypeDto dto)
    {
        var roomTypes = repository.GetAll();

        var newId = roomTypes.Count == 0
            ? 1
            : roomTypes.Max(roomType => roomType.Id) + 1;

        var roomType = new RoomType
        {
            Id = newId,
            Category = dto.Category,
            Area = dto.Area,
            BedCount = dto.BedCount,
            HasBathroom = dto.HasBathroom,
            PricePerDay = dto.PricePerDay
        };

        return ToDto(repository.Add(roomType));
    }

    /// <summary>
    /// Обновить тип номера
    /// </summary>
    public RoomTypeDto? Update(int id, CreateRoomTypeDto dto)
    {
        var roomType = new RoomType
        {
            Id = id,
            Category = dto.Category,
            Area = dto.Area,
            BedCount = dto.BedCount,
            HasBathroom = dto.HasBathroom,
            PricePerDay = dto.PricePerDay
        };

        var updatedRoomType = repository.Update(roomType);

        return updatedRoomType is null ? null : ToDto(updatedRoomType);
    }

    /// <summary>
    /// Удалить тип номера
    /// </summary>
    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    /// <summary>
    /// Преобразовать доменную сущность в DTO
    /// </summary>
    private static RoomTypeDto ToDto(RoomType roomType)
    {
        return new RoomTypeDto(
            roomType.Id,
            roomType.Category,
            roomType.Area,
            roomType.BedCount,
            roomType.HasBathroom,
            roomType.PricePerDay
        );
    }
}