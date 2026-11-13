using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с приёмами
/// </summary>
[ApiController]
[Route("api/appointments")]
public sealed class AppointmentController(IAppointmentService service) : ControllerBase
{
    /// <summary>
    /// Получить все приёмы
    /// </summary>
    [HttpGet]
    [EndpointSummary("Получить все приёмы")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<AppointmentDto>> GetAll() => Ok(service.Appointments());

    /// <summary>
    /// Получить приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Получить приём по идентификатору")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AppointmentDto> Get(int id)
    {
        var entity = service.Get(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать приём
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Создать приём")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AppointmentDto> Create(SaveAppointmentRequest request)
    {
        var entity = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить приём
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Обновить приём")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AppointmentDto> Update(int id, SaveAppointmentRequest request)
        => Ok(service.Update(id, request));

    /// <summary>
    /// Удалить приём
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Удалить приём")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.DeleteAppointment(id);
        return NoContent();
    }

    /// <summary>
    /// Получить приёмы за текущий месяц
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    [HttpGet("current-month")]
    [EndpointSummary("Получить приёмы за текущий месяц")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<AppointmentDto>> CurrentMonth([FromQuery] int roomNumber)
    {
        if (roomNumber <= 0)
            return BadRequest();

        return Ok(service.AppointmentsThisMonth(roomNumber, DateTime.Today));
    }
}
