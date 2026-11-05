using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Appointments;
using VetClinic.Contracts.Pets;

namespace VetClinic.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/pets")]
public class PetsController(IPetService pets, IAppointmentService appointments, ILogger<PetsController> logger)
    : ClinicControllerBase(logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await Execute("GET /api/pets", async () =>
        {
            logger.LogInformation("GET /api/pets");
            var result = await pets.GetAllAsync(cancellationToken);
            logger.LogInformation("GET /api/pets завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PetDto>> GetById(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/pets/{PetId}", async () =>
        {
            logger.LogInformation("GET /api/pets/{PetId}", id);
            var result = await pets.GetByIdAsync(id, cancellationToken);
            logger.LogInformation("GET /api/pets/{PetId} завершён", id);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}/appointments")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointments(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/pets/{PetId}/appointments", async () =>
        {
            logger.LogInformation("GET /api/pets/{PetId}/appointments", id);
            var result = await appointments.GetByPetAsync(id, cancellationToken);
            logger.LogInformation("GET /api/pets/{PetId}/appointments завершён, записей: {Count}", id, result.Count);
            return Ok(result);

        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PetDto>> Create(PetEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("POST /api/pets", async () =>
        {
            logger.LogInformation("POST /api/pets");
            var created = await pets.CreateAsync(dto, cancellationToken);
            logger.LogInformation("POST /api/pets завершён, идентификатор: {PetId}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PetDto>> Update(int id, PetEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("PUT /api/pets/{PetId}", async () =>
        {
            logger.LogInformation("PUT /api/pets/{PetId}", id);
            var updated = await pets.UpdateAsync(id, dto, cancellationToken);
            logger.LogInformation("PUT /api/pets/{PetId} завершён", id);
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
        return await Execute("DELETE /api/pets/{PetId}", async () =>
        {
            logger.LogInformation("DELETE /api/pets/{PetId}", id);
            await pets.DeleteAsync(id, cancellationToken);
            logger.LogInformation("DELETE /api/pets/{PetId} завершён", id);
            return NoContent();

        });
    }
}
