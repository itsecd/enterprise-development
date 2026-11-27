using Hotel.Application.Interfaces;
using Hotel.Application.Mapping;
using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;

namespace Hotel.Application.Services;

/// <summary>
/// Сервис для управления комнатами
/// </summary>
public class RoomService(IRepository<Room> repository, IRepository<RoomType> roomTypeRepository, IRepository<Booking> bookingRepository)
    : CrudService<Room, RoomRequest, RoomDto>(repository), IRoomService
{
    /// <summary>
    /// Создание сущности комнаты из запроса с проверкой существования типа комнаты
    /// </summary>
    protected override async Task<Room> BuildEntityAsync(RoomRequest request, int id)
    {
        var roomType = await roomTypeRepository.GetByIdAsync(request.RoomTypeId!.Value);
        if (roomType == null) throw new KeyNotFoundException($"Тип комнаты(Id={request.RoomTypeId}) не найден");

        return new Room
        {
            Id = id,
            Number = request.Number!.Value,
            Floor = request.Floor!.Value,
            HasBalcony = request.HasBalcony!.Value,
            RoomType = roomType
        };
    }

    /// <summary>
    /// Копирование обновленных значений в существующую сущность комнаты
    /// </summary>
    protected override void CopyValues(Room source, Room target)
    {
        target.Number = source.Number;
        target.Floor = source.Floor;
        target.HasBalcony = source.HasBalcony;
        target.RoomType = source.RoomType;
    }

    /// <summary>
    /// Преобразование сущности комнаты в DTO
    /// </summary>
    protected override RoomDto ToDto(Room entity) => entity.ToDto();

    /// <summary>
    /// Проверка комнаты на наличие бронирования перед удалением
    /// </summary>
    protected override async Task CheckDeleteAsync(Room entity)
    {
        var bookings = await bookingRepository.GetAllAsync();
        if (bookings.Any(b => b.Room.Id == entity.Id))
        {
            throw new ArgumentException("Нельзя удалить комнату, так как в ней проживает клиент");
        }
    }
}