namespace Hotel.Application.Contracts.RoomType; 
/// <summary>
/// Наследний интерфейса служды для CRUD операций над типами номеров
/// </summary>
public interface IRoomTypeService :
    IService<RoomTypeResponseDto, RoomTypeCreateUpdateDto>
{ }