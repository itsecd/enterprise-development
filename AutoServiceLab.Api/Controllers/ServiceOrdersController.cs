using AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;
using AutoServiceLab.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Контроллер для работы с заказами
/// </summary>
public class ServiceOrdersController(IServiceOrderService service, ILogger<ServiceOrdersController> logger)
    : CrudControllerBase<ServiceOrderDto, ServiceOrderCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    [HttpGet]
    public override async Task<ActionResult<IList<ServiceOrderDto>>> GetAll() => await base.GetAll();

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override async Task<ActionResult<ServiceOrderDto>> Get(int id) => await base.Get(id);

    /// <summary>
    /// Создать новый заказ
    /// </summary>
    [HttpPost]
    public override async Task<ActionResult<ServiceOrderDto>> Post(ServiceOrderCreateUpdateDto dto) => await base.Post(dto);

    /// <summary>
    /// Обновить данные заказа
    /// </summary>
    [HttpPut("{id}")]
    public override async Task<ActionResult<ServiceOrderDto>> Update(ServiceOrderCreateUpdateDto dto, int id) => await base.Update(dto, id);

    /// <summary>
    /// Удалить заказ
    /// </summary>
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(int id) => await base.Delete(id);

    /// <summary>
    /// Получить работы заказа
    /// </summary>
    [HttpGet("{id}/works")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<string>>> GetWorks(int id)
    {
        logger.LogInformation("Запрос на получение работ заказа {Id}", id);
        return Ok(await service.GetWorksByOrder(id));
    }
}
