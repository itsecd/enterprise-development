using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;

namespace Hotel.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления типами комнат
/// </summary>
public interface IRoomTypeService : ICrudService<RoomTypeRequest, RoomTypeDto>
{
}