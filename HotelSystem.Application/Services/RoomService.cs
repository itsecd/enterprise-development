using HotelSystem.Application.Contracts.Rooms;
using HotelSystem.Application.Exceptions;
using HotelSystem.Application.Mapping;
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с номерами гостиницы
/// </summary>
public class RoomService(
    IRepository<Room> roomRepository,
    IRepository<RoomType> roomTypeRepository)
    : ICrudService<RoomRequest, RoomResponse>
{
    /// <inheritdoc />
    public List<RoomResponse> GetAll() =>
        roomRepository.GetAll().Select(room => room.ToResponse()).ToList();

    /// <inheritdoc />
    public RoomResponse? GetById(int id) =>
        roomRepository.GetById(id)?.ToResponse();

    /// <inheritdoc />
    public RoomResponse Add(RoomRequest request) =>
        roomRepository.Add(CreateEntity(request)).ToResponse();

    /// <inheritdoc />
    public bool Update(int id, RoomRequest request)
    {
        if (roomRepository.GetById(id) is null)
        {
            return false;
        }

        return roomRepository.Update(id, CreateEntity(request));
    }

    /// <inheritdoc />
    public bool Delete(int id) =>
        roomRepository.Delete(id);

    private Room CreateEntity(RoomRequest request)
    {
        var roomType = roomTypeRepository.GetById(request.RoomTypeId)
            ?? throw new ReferenceNotFoundException("Тип номера", request.RoomTypeId);

        return request.ToEntity(roomType);
    }
}