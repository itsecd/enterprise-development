using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Appointments;

namespace VetClinic.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/appointments")]
public class AppointmentsController(IAppointmentService appointments, ILogger<AppointmentsController> logger)
    : ClinicControllerBase(logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await Execute("GET /api/appointments", async () =>
        {
            logger.LogInformation("GET /api/appointments");
            var result = await appointments.GetAllAsync(cancellationToken);
            logger.LogInformation("GET /api/appointments завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> GetById(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/appointments/{AppointmentId}", async () =>
        {
            logger.LogInformation("GET /api/appointments/{AppointmentId}", id);
            var result = await appointments.GetByIdAsync(id, cancellationToken);
            logger.LogInformation("GET /api/appointments/{AppointmentId} завершён", id);
            return Ok(result);

        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> Create(AppointmentEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("POST /api/appointments", async () =>
        {
            logger.LogInformation("POST /api/appointments");
            var created = await appointments.CreateAsync(dto, cancellationToken);
            logger.LogInformation("POST /api/appointments завершён, идентификатор: {AppointmentId}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> Update(int id, AppointmentEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("PUT /api/appointments/{AppointmentId}", async () =>
        {
            logger.LogInformation("PUT /api/appointments/{AppointmentId}", id);
            var updated = await appointments.UpdateAsync(id, dto, cancellationToken);
            logger.LogInformation("PUT /api/appointments/{AppointmentId} завершён", id);
            return Ok(updated);

        });
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return await Execute("DELETE /api/appointments/{AppointmentId}", async () =>
        {
            logger.LogInformation("DELETE /api/appointments/{AppointmentId}", id);
            await appointments.DeleteAsync(id, cancellationToken);
            logger.LogInformation("DELETE /api/appointments/{AppointmentId} завершён", id);
            return NoContent();

        });
    }
}
