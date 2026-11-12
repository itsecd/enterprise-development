using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Services;
using BeautySalon.Contracts.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// HTTP операции для ресурса services
/// </summary>
/// <param name="service">Сервис приложения</param>
/// <param name="appointments">Сервис записей</param>
[ApiController]
[Route("api/services")]
[Produces("application/json")]
public sealed class ServicesController(
    IServiceService service, IAppointmentService appointments) : CrudController<SaveServiceRequest, ServiceResponse>(service)
{
    /// <summary>
    /// Получить связанные записи
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    [HttpGet("{id:int}/appointments")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<AppointmentResponse>> GetAppointments(int id)
    {
        return Ok(appointments.GetByService(id));
    }
}
