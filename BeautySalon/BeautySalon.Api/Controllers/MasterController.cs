using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Masters;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над мастерами
/// </summary>
/// <param name="masterService">Служба для мастеров</param>
/// <param name="logger">Логгер</param>
public class MasterController(IApplicationService<MasterDto, MasterCreateUpdateDto, int> masterService,
    ILogger<MasterController> logger)
    : CrudControllerBase<MasterDto, MasterCreateUpdateDto, int>(masterService, logger);
