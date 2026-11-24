using HotelSystem.Application.Contracts.RoomTypes;
using HotelSystem.Application.Mapping;
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с типами номеров
/// </summary>
public class RoomTypeService(IRepository<RoomType> roomTypeRepository)
    : ICrudService<RoomTypeRequest, RoomTypeResponse>
{
    /// <inheritdoc />
    public async Task<List<RoomTypeResponse>> GetAllAsync() =>
        (await roomTypeRepository.GetAllAsync())
            .Select(roomType => roomType.ToResponse())
            .ToList();

    /// <inheritdoc />
    public async Task<RoomTypeResponse?> GetByIdAsync(int id) =>
        (await roomTypeRepository.GetByIdAsync(id))?.ToResponse();

    /// <inheritdoc />
    public async Task<RoomTypeResponse> AddAsync(RoomTypeRequest request) =>
        (await roomTypeRepository.AddAsync(request.ToEntity())).ToResponse();

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, RoomTypeRequest request) =>
        await roomTypeRepository.UpdateAsync(id, request.ToEntity());

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id) =>
        await roomTypeRepository.DeleteAsync(id);
}