using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts.Appointments;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с приёмами
/// </summary>
[ApiController]
[Route("api/appointments")]
public sealed class AppointmentController(
    IAppointmentService service,
    ILogger<AppointmentController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все приёмы
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all appointments")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAll()
    {
        logger.LogInformation("Получение списка приёмов");

        var result = await service.AppointmentsAsync(HttpContext.RequestAborted);

        logger.LogInformation("Получено приёмов: {Count}", result.Count);
        return Ok(result);
    }

    /// <summary>
    /// Получить приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get appointment by ID")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> Get(int id)
    {
        logger.LogInformation("Получение приёма с Id {Id}", id);

        var result = await service.GetAsync(id, HttpContext.RequestAborted);
        if (result is null)
        {
            logger.LogWarning("Приём с Id {Id} не найден", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создать приём
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Create appointment")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppointmentDto>> Create(SaveAppointmentRequest request)
    {
        logger.LogInformation("Создание нового приёма");

        var result = await service.CreateAsync(request, HttpContext.RequestAborted);

        logger.LogInformation("Приём с Id {Id} создан", result.Id);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновить приём
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update appointment")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> Update(int id, SaveAppointmentRequest request)
    {
        logger.LogInformation("Обновление приёма с Id {Id}", id);

        var result = await service.UpdateAsync(id, request, HttpContext.RequestAborted);

        logger.LogInformation("Приём с Id {Id} обновлён", id);
        return Ok(result);
    }

    /// <summary>
    /// Удалить приём
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete appointment")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("Удаление приёма с Id {Id}", id);

        await service.DeleteAppointmentAsync(id, HttpContext.RequestAborted);

        logger.LogInformation("Приём с Id {Id} удалён", id);
        return NoContent();
    }

    /// <summary>
    /// Получить приёмы за текущий месяц
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    [HttpGet("current-month")]
    [EndpointSummary("Get appointments for the current month")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> CurrentMonth([FromQuery] int roomNumber)
    {
        if (roomNumber <= 0)
        {
            logger.LogWarning("Передан некорректный номер кабинета {RoomNumber}", roomNumber);
            return BadRequest();
        }

        logger.LogInformation(
            "Получение приёмов за текущий месяц для кабинета {RoomNumber}",
            roomNumber);

        var result = await service.AppointmentsThisMonthAsync(
            roomNumber,
            DateTime.Today,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для кабинета {RoomNumber} найдено приёмов: {Count}",
            roomNumber,
            result.Count);

        return Ok(result);
    }
}
