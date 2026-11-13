using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Appointments;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над записями
/// </summary>
/// <param name="appointmentService">Служба для записей</param>
/// <param name="logger">Логгер</param>
public class AppointmentController(IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int> appointmentService,
    ILogger<AppointmentController> logger)
    : CrudControllerBase<AppointmentDto, AppointmentCreateUpdateDto, int>(appointmentService, logger);
