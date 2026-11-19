using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Customers;
using BeautySalon.Contracts.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// HTTP операции для ресурса customers
/// </summary>
/// <param name="service">Сервис приложения</param>
/// <param name="appointments">Сервис записей</param>
[ApiController]
[Route("api/customers")]
[Produces("application/json")]
public sealed class CustomersController(
    ICustomerService service, IAppointmentService appointments) : CrudController<SaveCustomerRequest, CustomerResponse>(service)
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
        return Ok(appointments.GetByCustomer(id));
    }
}
