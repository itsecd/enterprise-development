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
    public override ActionResult<IList<ServiceOrderDto>> GetAll() => base.GetAll();

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override ActionResult<ServiceOrderDto> Get(int id) => base.Get(id);

    /// <summary>
    /// Создать новый заказ
    /// </summary>
    [HttpPost]
    public override ActionResult<ServiceOrderDto> Post(ServiceOrderCreateUpdateDto dto) => base.Post(dto);

    /// <summary>
    /// Обновить данные заказа
    /// </summary>
    [HttpPut("{id}")]
    public override ActionResult<ServiceOrderDto> Update(ServiceOrderCreateUpdateDto dto, int id) => base.Update(dto, id);

    /// <summary>
    /// Удалить заказ
    /// </summary>
    [HttpDelete("{id}")]
    public override IActionResult Delete(int id) => base.Delete(id);

    /// <summary>
    /// Получить работы заказа
    /// </summary>
    [HttpGet("{id}/works")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<string>> GetWorks(int id)
    {
        logger.LogInformation("Запрос на получение работ заказа {Id}", id);
        return Ok(service.GetWorksByOrder(id));
    }
}
