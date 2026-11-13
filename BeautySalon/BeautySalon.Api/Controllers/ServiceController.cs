using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Services;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над услугами
/// </summary>
/// <param name="serviceService">Служба для услуг</param>
/// <param name="logger">Логгер</param>
public class ServiceController(IApplicationService<ServiceDto, ServiceCreateUpdateDto, int> serviceService,
    ILogger<ServiceController> logger)
    : CrudControllerBase<ServiceDto, ServiceCreateUpdateDto, int>(serviceService, logger);
