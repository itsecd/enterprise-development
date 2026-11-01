using AutoServiceLab.Application.Contracts.DTOs.Cars;
using AutoServiceLab.Application.Contracts.Interfaces;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Контроллер для работы с автомобилями
/// </summary>
public class CarsController(ICarService service, ILogger<CarsController> logger)
    : CrudControllerBase<CarDto, CarCreateUpdateDto, int>(service, logger)
{
}
