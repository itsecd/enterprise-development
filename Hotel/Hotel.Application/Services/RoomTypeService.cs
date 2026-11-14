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
    public async Task<IReadOnlyList<RoomTypeDto>> GetAll()
    {
        var roomTypes = await repository.GetAll();

        return roomTypes
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить тип номера по идентификатору
    /// </summary>
    public async Task<RoomTypeDto?> GetById(int id)
    {
        var roomType = await repository.GetById(id);

        return roomType is null ? null : ToDto(roomType);
    }

    /// <summary>
    /// Создать тип номера
    /// </summary>
    public async Task<RoomTypeDto> Create(CreateRoomTypeDto dto)
    {
        var roomType = new RoomType
        {
            Id = 0,
            Category = dto.Category,
            Area = dto.Area,
            BedCount = dto.BedCount,
            HasBathroom = dto.HasBathroom,
            PricePerDay = dto.PricePerDay
        };

        var createdRoomType = await repository.Add(roomType);

        return ToDto(createdRoomType);
    }

    /// <summary>
    /// Обновить тип номера
    /// </summary>
    public async Task<RoomTypeDto?> Update(int id, CreateRoomTypeDto dto)
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

        var updatedRoomType = await repository.Update(roomType);

        return updatedRoomType is null ? null : ToDto(updatedRoomType);
    }

    /// <summary>
    /// Удалить тип номера
    /// </summary>
    public Task<bool> Delete(int id)
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