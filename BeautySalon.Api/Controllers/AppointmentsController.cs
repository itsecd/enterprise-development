using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// HTTP операции для ресурса appointments
/// </summary>
/// <param name="service">Сервис приложения</param>

[ApiController]
[Route("api/appointments")]
[Produces("application/json")]
public sealed class AppointmentsController(
    IAppointmentService service) : CrudController<SaveAppointmentRequest, AppointmentResponse>(service)
{
    /// <inheritdoc />
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public override ActionResult<AppointmentResponse> Create(SaveAppointmentRequest request)
    {
        return base.Create(request);
    }
}
