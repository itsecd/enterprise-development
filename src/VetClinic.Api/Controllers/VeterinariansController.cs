using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Appointments;
using VetClinic.Contracts.Pets;
using VetClinic.Contracts.Veterinarians;

namespace VetClinic.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/veterinarians")]
public class VeterinariansController(
    IVeterinarianService veterinarians,
    IPetService pets,
    IAppointmentService appointments,
    ILogger<VeterinariansController> logger) : ClinicControllerBase(logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await Execute("GET /api/veterinarians", async () =>
        {
            logger.LogInformation("GET /api/veterinarians");
            var result = await veterinarians.GetAllAsync(cancellationToken);
            logger.LogInformation("GET /api/veterinarians завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VeterinarianDto>> GetById(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/veterinarians/{VeterinarianId}", async () =>
        {
            logger.LogInformation("GET /api/veterinarians/{VeterinarianId}", id);
            var result = await veterinarians.GetByIdAsync(id, cancellationToken);
            logger.LogInformation("GET /api/veterinarians/{VeterinarianId} завершён", id);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}/pets")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPets(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/veterinarians/{VeterinarianId}/pets", async () =>
        {
            logger.LogInformation("GET /api/veterinarians/{VeterinarianId}/pets", id);
            var result = await pets.GetByVeterinarianAsync(id, cancellationToken);
            logger.LogInformation(
                "GET /api/veterinarians/{VeterinarianId}/pets завершён, записей: {Count}",
                id,
                result.Count);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}/appointments")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointments(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/veterinarians/{VeterinarianId}/appointments", async () =>
        {
            logger.LogInformation("GET /api/veterinarians/{VeterinarianId}/appointments", id);
            var result = await appointments.GetByVeterinarianAsync(id, cancellationToken);
            logger.LogInformation(
                "GET /api/veterinarians/{VeterinarianId}/appointments завершён, записей: {Count}",
                id,
                result.Count);
            return Ok(result);

        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VeterinarianDto>> Create(VeterinarianEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("POST /api/veterinarians", async () =>
        {
            logger.LogInformation("POST /api/veterinarians");
            var created = await veterinarians.CreateAsync(dto, cancellationToken);
            logger.LogInformation("POST /api/veterinarians завершён, идентификатор: {VeterinarianId}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VeterinarianDto>> Update(int id, VeterinarianEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("PUT /api/veterinarians/{VeterinarianId}", async () =>
        {
            logger.LogInformation("PUT /api/veterinarians/{VeterinarianId}", id);
            var updated = await veterinarians.UpdateAsync(id, dto, cancellationToken);
            logger.LogInformation("PUT /api/veterinarians/{VeterinarianId} завершён", id);
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
        return await Execute("DELETE /api/veterinarians/{VeterinarianId}", async () =>
        {
            logger.LogInformation("DELETE /api/veterinarians/{VeterinarianId}", id);
            await veterinarians.DeleteAsync(id, cancellationToken);
            logger.LogInformation("DELETE /api/veterinarians/{VeterinarianId} завершён", id);
            return NoContent();

        });
    }
}
