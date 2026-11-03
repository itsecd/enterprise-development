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
    public List<RoomTypeResponse> GetAll() =>
        roomTypeRepository.GetAll().Select(roomType => roomType.ToResponse()).ToList();

    /// <inheritdoc />
    public RoomTypeResponse? GetById(int id) =>
        roomTypeRepository.GetById(id)?.ToResponse();

    /// <inheritdoc />
    public RoomTypeResponse Add(RoomTypeRequest request) =>
        roomTypeRepository.Add(request.ToEntity()).ToResponse();

    /// <inheritdoc />
    public bool Update(int id, RoomTypeRequest request) =>
        roomTypeRepository.Update(id, request.ToEntity());

    /// <inheritdoc />
    public bool Delete(int id) =>
        roomTypeRepository.Delete(id);
}