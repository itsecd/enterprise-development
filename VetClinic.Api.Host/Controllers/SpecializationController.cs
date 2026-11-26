using VetClinic.Application.Contracts.Specializations;

namespace VetClinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления справочником врачебных специализаций
/// </summary>
/// <param name="service">Сервис управления специализациями</param>
/// <param name="logger">Логгер контроллера специализаций</param>
public class SpecializationController(ISpecializationAppService service, ILogger<SpecializationController> logger)
    : CrudControllerBase<SpecializationDto, SpecializationCreateUpdateDto, int>(service, logger);