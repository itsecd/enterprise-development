using AutoServiceLab.Application.Contracts.Dtos.Mechanics;
using AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;
using AutoServiceLab.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Контроллер для работы с механиками
/// </summary>
public class MechanicsController(IMechanicService service, ILogger<MechanicsController> logger)
    : CrudControllerBase<MechanicDto, MechanicCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Получить список всех механиков
    /// </summary>
    [HttpGet]
    public override ActionResult<IList<MechanicDto>> GetAll() => base.GetAll();

    /// <summary>
    /// Получить механика по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override ActionResult<MechanicDto> Get(int id) => base.Get(id);

    /// <summary>
    /// Создать нового механика
    /// </summary>
    [HttpPost]
    public override ActionResult<MechanicDto> Post(MechanicCreateUpdateDto dto) => base.Post(dto);

    /// <summary>
    /// Обновить данные механика
    /// </summary>
    [HttpPut("{id}")]
    public override ActionResult<MechanicDto> Update(MechanicCreateUpdateDto dto, int id) => base.Update(dto, id);

    /// <summary>
    /// Удалить механика
    /// </summary>
    [HttpDelete("{id}")]
    public override IActionResult Delete(int id) => base.Delete(id);

    /// <summary>
    /// Получить заказы механика
    /// </summary>
    [HttpGet("{id}/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<ServiceOrderDto>> GetMechanicOrders(int id)
    {
        logger.LogInformation("Запрос на получение заказов механика {Id}", id);
        return Ok(service.GetOrdersByMechanic(id));
    }
}
