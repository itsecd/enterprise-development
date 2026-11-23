using AutoServiceLab.Application.Contracts.Dtos.WorkTypes;
using AutoServiceLab.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Контроллер для работы с видами работ
/// </summary>
public class WorkTypesController(IWorkTypeService service, ILogger<WorkTypesController> logger)
    : CrudControllerBase<WorkTypeDto, WorkTypeCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Получить список всех видов работ
    /// </summary>
    [HttpGet]
    public override async Task<ActionResult<IList<WorkTypeDto>>> GetAll() => await base.GetAll();

    /// <summary>
    /// Получить вид работ по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override async Task<ActionResult<WorkTypeDto>> Get(int id) => await base.Get(id);

    /// <summary>
    /// Создать новый вид работ
    /// </summary>
    [HttpPost]
    public override async Task<ActionResult<WorkTypeDto>> Post(WorkTypeCreateUpdateDto dto) => await base.Post(dto);

    /// <summary>
    /// Обновить данные вида работ
    /// </summary>
    [HttpPut("{id}")]
    public override async Task<ActionResult<WorkTypeDto>> Update(WorkTypeCreateUpdateDto dto, int id) => await base.Update(dto, id);

    /// <summary>
    /// Удалить вид работ
    /// </summary>
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(int id) => await base.Delete(id);
}
