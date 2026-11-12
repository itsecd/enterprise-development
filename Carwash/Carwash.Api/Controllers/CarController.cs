using Carwash.Application.Contracts;
using Carwash.Application.Contracts.Cars;

namespace Carwash.Api.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над автомобилями
/// </summary>
/// <param name="service">Служба для CRUD операций с Dto автомобилей</param>
/// <param name="logger">Логгер</param>
public class CarController(IApplicationService<CarDto, CarCreateUpdateDto, int> service, ILogger<CarController> logger)
    : CrudControllerBase<CarDto, CarCreateUpdateDto, int>(service, logger);
