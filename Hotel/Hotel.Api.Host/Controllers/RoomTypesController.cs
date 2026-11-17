using Hotel.Application.Contracts.RoomTypes;

namespace Hotel.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления типами номеров
/// </summary>
public class RoomTypesController(IRoomTypeService appService, ILogger<RoomTypesController> logger)
    : CrudControllerBase<RoomTypeDto, CreateRoomTypeDto, int>(appService, logger);