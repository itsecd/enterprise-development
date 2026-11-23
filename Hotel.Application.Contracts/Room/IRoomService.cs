namespace Hotel.Application.Contracts.Room;
/// <summary>
/// Наследний интерфейса служды для CRUD операций над номерами
/// </summary>
public interface IRoomService :
    IService<RoomResponseDto, RoomCreateUpdateDto>
{ }