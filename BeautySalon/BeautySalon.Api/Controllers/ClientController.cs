using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Clients;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над клиентами
/// </summary>
/// <param name="clientService">Служба для клиентов</param>
/// <param name="logger">Логгер</param>
public class ClientController(IApplicationService<ClientDto, ClientCreateUpdateDto, int> clientService,
    ILogger<ClientController> logger)
    : CrudControllerBase<ClientDto, ClientCreateUpdateDto, int>(clientService, logger);
