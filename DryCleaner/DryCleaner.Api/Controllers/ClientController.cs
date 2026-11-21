using DryCleaner.Application.Contracts.Client;
using DryCleaner.Application.Contracts.ServiceInterfaces;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами.
/// </summary>
/// <param name="service">Сервис для работы с клиентами.</param>
/// <param name="logger">Логгер для записи событий контроллера.</param>
public class ClientController(IClientService service, ILogger<ClientController> logger) :
    CrudControllerBase<ClientDto, ClientCreateUpdateDto, int>(service, logger);
