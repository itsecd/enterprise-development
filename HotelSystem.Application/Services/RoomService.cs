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
    public async Task<List<RoomResponse>> GetAllAsync() =>
        (await roomRepository.GetAllAsync())
            .Select(room => room.ToResponse())
            .ToList();

    /// <inheritdoc />
    public async Task<RoomResponse?> GetByIdAsync(int id) =>
        (await roomRepository.GetByIdAsync(id))?.ToResponse();

    /// <inheritdoc />
    public async Task<RoomResponse> AddAsync(RoomRequest request) =>
        (await roomRepository.AddAsync(await CreateEntityAsync(request))).ToResponse();

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, RoomRequest request)
    {
        if (await roomRepository.GetByIdAsync(id) is null)
        {
            return false;
        }

        return await roomRepository.UpdateAsync(id, await CreateEntityAsync(request));
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id) =>
        await roomRepository.DeleteAsync(id);

    private async Task<Room> CreateEntityAsync(RoomRequest request)
    {
        var roomType = await roomTypeRepository.GetByIdAsync(request.RoomTypeId)
            ?? throw new ReferenceNotFoundException("Тип номера", request.RoomTypeId);

        return request.ToEntity(roomType);
    }
}