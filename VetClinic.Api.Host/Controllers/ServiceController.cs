using VetClinic.Application.Contracts.Services;

namespace VetClinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления перечнем медицинских услуг и прейскурантом клиники
/// </summary>
/// <param name="service">Сервис управления услугами</param>
/// <param name="logger">Логгер контроллера услуг</param>
public class ServiceController(IServiceAppService service, ILogger<ServiceController> logger)
    : CrudControllerBase<ServiceDto, ServiceCreateUpdateDto, int>(service, logger);