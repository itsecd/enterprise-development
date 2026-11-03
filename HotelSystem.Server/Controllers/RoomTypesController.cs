using HotelSystem.Application.Contracts.RoomTypes;
using HotelSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для управления типами номеров гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomTypesController(
    RoomTypeService roomTypeService,
    ILogger<RoomTypesController> logger)
    : BaseCrudController<RoomTypeRequest, RoomTypeResponse>(roomTypeService, logger);