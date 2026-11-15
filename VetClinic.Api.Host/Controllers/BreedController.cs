using VetClinic.Application.Contracts.Breeds;

namespace VetClinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления справочником пород животных
/// </summary>
/// <param name="service">Сервис управления породами</param>
/// <param name="logger">Логгер контроллера пород</param>
public class BreedController(IBreedAppService service, ILogger<BreedController> logger)
    : CrudControllerBase<BreedDto, BreedCreateUpdateDto, int>(service, logger);