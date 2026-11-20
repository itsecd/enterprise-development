using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Dto.Appointments;
using VetClinic.Contracts.Services;

namespace VetClinic.Api.Controllers;

/// <summary>
/// Записи на прием
/// </summary>
/// <param name="service">Сервис</param>
/// <param name="logger">Логгер</param>
[Route("api/appointments")]
[Tags("Записи на прием")]
public class AppointmentsController(IAppointmentService service, ILogger<AppointmentsController> logger)
    : CrudControllerBase<AppointmentDto, AppointmentEditDto>(service, logger)
{
    /// <inheritdoc/>
    protected override int GetId(AppointmentDto dto) => dto.Id;
}
