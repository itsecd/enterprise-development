using Carwash.Application.Contracts;
using Carwash.Application.Contracts.Services;

namespace Carwash.Api.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над услугами
/// </summary>
/// <param name="service">Служба для CRUD операций с Dto услуг</param>
/// <param name="logger">Логгер</param>
public class ServiceController(IApplicationService<ServiceDto, ServiceCreateUpdateDto, int> service, ILogger<ServiceController> logger)
    : CrudControllerBase<ServiceDto, ServiceCreateUpdateDto, int>(service, logger);
