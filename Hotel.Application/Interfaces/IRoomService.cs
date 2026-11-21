using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;

namespace Hotel.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления комнатами
/// </summary>
public interface IRoomService : ICrudService<RoomRequest, RoomDto>
{
}