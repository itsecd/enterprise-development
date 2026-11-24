using System.ComponentModel.DataAnnotations;
using Hotel.Contracts.Dto;
using Hotel.Contracts.Exceptions;
using Hotel.Contracts.Interfaces;
using Hotel.Domain.Enums;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Models;

namespace Hotel.Application.Services;

/// <summary>Сервис справочника типов номеров.</summary>
/// <param name="repository">Репозиторий типов номеров.</param>
/// <param name="roomRepository">Репозиторий гостиничных номеров.</param>
/// <param name="relationshipLock">Защита связанных операций.</param>
public class RoomTypeService(
    IRoomTypeRepository repository,
    IRoomRepository roomRepository,
    IRelationshipLock relationshipLock) : IRoomTypeService
{
    private readonly IRoomTypeRepository _repository = repository;
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IRelationshipLock _relationshipLock = relationshipLock;

    public async Task<IReadOnlyList<RoomTypeDto>> GetAllAsync()
    {
        var roomTypes = await _repository.GetAllAsync();
        return roomTypes.OrderBy(type => type.Id).Select(MapToDto).ToList();
    }

    public async Task<RoomTypeDto?> GetByIdAsync(int id)
    {
        var roomType = await _repository.GetByIdAsync(id);
        return roomType is null ? null : MapToDto(roomType);
    }

    public async Task<RoomTypeDto> AddAsync(RoomTypeEditDto request)
    {
        ValidateRoomType(request);
        using var operation = await _relationshipLock.AcquireAsync();

        var roomType = new RoomType
        {
            Category = request.Category,
            Area = request.Area,
            BedCount = request.BedCount,
            HasBathroom = request.HasBathroom,
            PricePerNight = request.PricePerNight
        };

        return MapToDto(await _repository.AddAsync(roomType));
    }

    public async Task<bool> UpdateAsync(int id, RoomTypeEditDto request)
    {
        ValidateRoomType(request);
        using var operation = await _relationshipLock.AcquireAsync();

        if (await _repository.GetByIdAsync(id) is null)
            return false;

        var roomType = new RoomType
        {
            Id = id,
            Category = request.Category,
            Area = request.Area,
            BedCount = request.BedCount,
            HasBathroom = request.HasBathroom,
            PricePerNight = request.PricePerNight
        };

        return await _repository.UpdateAsync(roomType);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var operation = await _relationshipLock.AcquireAsync();

        if (await _repository.GetByIdAsync(id) is null)
            return false;

        if (await _roomRepository.HasRoomsForTypeAsync(id))
            throw new ResourceConflictException("Нельзя удалить тип номера, который используется гостиничными номерами.");

        return await _repository.DeleteAsync(id);
    }

    private static void ValidateRoomType(RoomTypeEditDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);

        if (!Enum.IsDefined(request.Category))
            throw new ValidationException("Указана неизвестная категория номера.");
        if (request.Area <= 0 || request.BedCount <= 0 || request.PricePerNight <= 0)
            throw new ValidationException("Площадь, количество кроватей и стоимость должны быть положительными.");
    }

    private static RoomTypeDto MapToDto(RoomType roomType)
    {
        return new RoomTypeDto
        {
            Id = roomType.Id,
            Category = roomType.Category,
            Area = roomType.Area,
            BedCount = roomType.BedCount,
            HasBathroom = roomType.HasBathroom,
            PricePerNight = roomType.PricePerNight
        };
    }
}
