namespace Hotel.Application.Contracts.RoomTypes;

/// <summary>
/// Интерфейс для работы с типами номеров
/// </summary>
public interface IRoomTypeService
    : ICrudService<RoomTypeDto, CreateRoomTypeDto, int>
{
}
