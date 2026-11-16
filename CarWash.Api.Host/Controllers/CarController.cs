using CarWash.Application.Contracts;
using CarWash.Application.Contracts.Cars;

namespace CarWash.Api.Host.Controllers;

/// <summary>
/// Контроллер CRUD-операций над автомобилями
/// </summary>
public class CarController(
    IApplicationService<CarDto, CarCreateUpdateDto, int> crudService,
    ILogger<CarController> logger)
    : CrudControllerBase<CarDto, CarCreateUpdateDto, int>(
        crudService, logger);