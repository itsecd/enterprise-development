using AutoServiceLab.Application.Contracts.DTOs.WorkTypes;
using AutoServiceLab.Application.Contracts.Interfaces;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Контроллер для работы с видами работ
/// </summary>
public class WorkTypesController(IWorkTypeService service, ILogger<WorkTypesController> logger)
    : CrudControllerBase<WorkTypeDto, WorkTypeCreateUpdateDto, int>(service, logger)
{
}
