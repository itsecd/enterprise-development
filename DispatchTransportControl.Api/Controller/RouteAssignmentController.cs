using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DispatchTransportControl.Api.Controller;

/// <summary>
///     Контроллер для назначений водителей и транспортных средств на маршруты
/// </summary>
[Route("api/route-assignment")]
[ApiController]
public class RouteAssignmentController(IRouteAssignmentService service) : ControllerBase
{
    /// <summary>
    ///     Получение всех назначений водителей и транспортных средств на маршруты
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<RouteAssignmentDto>> GetRouteAssignments()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    ///     Получение назначения водителя и транспортного средства на маршрут по id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<RouteAssignmentDto> GetRouteAssignment(int id)
    {
        var result = service.GetById(id);
        if (result == null) return NotFound();

        return Ok(result);
    }

    /// <summary>
    ///     Создание назначения водителя и транспортного средства на маршрут
    /// </summary>
    [HttpPost]
    public ActionResult<RouteAssignmentDto> CreateRouteAssignment(RouteAssignmentCreateDto vehicle)
    {
        var result = service.Create(vehicle);
        return CreatedAtAction(nameof(GetRouteAssignment), new { id = result.Id }, result);
    }

    /// <summary>
    ///     Изменение существующего назначения водителя и транспортного средства на маршрут
    /// </summary>
    [HttpPut]
    public ActionResult<RouteAssignmentDto> UpdateRouteAssignment(RouteAssignmentUpdateDto vehicle)
    {
        return Ok(service.Update(vehicle));
    }

    /// <summary>
    ///     Удаление назначения водителя и транспортного средства на маршрут
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteRouteAssignment(int id)
    {
        service.Delete(id);
        return Ok();
    }
}