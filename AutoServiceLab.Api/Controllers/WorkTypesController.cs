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
    public override ActionResult<IList<WorkTypeDto>> GetAll() => base.GetAll();

    /// <summary>
    /// Получить вид работ по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override ActionResult<WorkTypeDto> Get(int id) => base.Get(id);

    /// <summary>
    /// Создать новый вид работ
    /// </summary>
    [HttpPost]
    public override ActionResult<WorkTypeDto> Post(WorkTypeCreateUpdateDto dto) => base.Post(dto);

    /// <summary>
    /// Обновить данные вида работ
    /// </summary>
    [HttpPut("{id}")]
    public override ActionResult<WorkTypeDto> Update(WorkTypeCreateUpdateDto dto, int id) => base.Update(dto, id);

    /// <summary>
    /// Удалить вид работ
    /// </summary>
    [HttpDelete("{id}")]
    public override IActionResult Delete(int id) => base.Delete(id);
}
