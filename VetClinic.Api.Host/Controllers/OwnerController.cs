using VetClinic.Application.Contracts.Owners;

namespace VetClinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления профилями владельцев животных
/// </summary>
/// <param name="service">Сервис управления клиентами клиники</param>
/// <param name="logger">Логгер контроллера владельцев</param>
public class OwnerController(IOwnerAppService service, ILogger<OwnerController> logger)
    : CrudControllerBase<OwnerDto, OwnerCreateUpdateDto, int>(service, logger);
