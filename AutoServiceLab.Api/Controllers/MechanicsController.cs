using AutoServiceLab.Application.Contracts.DTOs.Mechanics;
using AutoServiceLab.Application.Contracts.DTOs.ServiceOrders;
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
