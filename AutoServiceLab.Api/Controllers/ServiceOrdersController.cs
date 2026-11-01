using AutoServiceLab.Application.Contracts.DTOs.ServiceOrders;
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
