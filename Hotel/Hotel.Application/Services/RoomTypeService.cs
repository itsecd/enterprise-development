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
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<RoomTypeDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var roomTypes = await repository.GetAll(cancellationToken);

        return roomTypes
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить тип номера по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<RoomTypeDto?> GetById(int id, CancellationToken cancellationToken = default)
    {
        var roomType = await repository.GetById(id, cancellationToken);

        return roomType is null ? null : ToDto(roomType);
    }

    /// <summary>
    /// Создать тип номера
    /// </summary>
    /// <param name="dto">Данные для создания типа номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<RoomTypeDto> Create(CreateRoomTypeDto dto, CancellationToken cancellationToken = default)
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

        var createdRoomType = await repository.Add(roomType, cancellationToken);

        return ToDto(createdRoomType);
    }

    /// <summary>
    /// Обновить тип номера
    /// </summary>
    /// <param name="id">Идентификатор типа номера</param>
    /// <param name="dto">Данные для обновления типа номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<RoomTypeDto?> Update(int id, CreateRoomTypeDto dto, CancellationToken cancellationToken = default)
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

        var updatedRoomType = await repository.Update(roomType, cancellationToken);

        return updatedRoomType is null ? null : ToDto(updatedRoomType);
    }

    /// <summary>
    /// Удалить тип номера
    /// </summary>
    /// <param name="id">Идентификатор типа номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        return repository.Delete(id, cancellationToken);
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