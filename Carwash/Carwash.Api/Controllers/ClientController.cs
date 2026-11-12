using Carwash.Application.Contracts;
using Carwash.Application.Contracts.Clients;

namespace Carwash.Api.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над клиентами
/// </summary>
/// <param name="service">Служба для CRUD операций с Dto клиентов</param>
/// <param name="logger">Логгер</param>
public class ClientController(IApplicationService<ClientDto, ClientCreateUpdateDto, int> service, ILogger<ClientController> logger)
    : CrudControllerBase<ClientDto, ClientCreateUpdateDto, int>(service, logger);
