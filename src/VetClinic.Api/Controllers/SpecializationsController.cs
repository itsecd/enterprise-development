using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Specializations;
using VetClinic.Contracts.Veterinarians;

namespace VetClinic.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/specializations")]
public class SpecializationsController(
    ISpecializationService specializations,
    IVeterinarianService veterinarians,
    ILogger<SpecializationsController> logger) : ClinicControllerBase(logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await Execute("GET /api/specializations", async () =>
        {
            logger.LogInformation("GET /api/specializations");
            var result = await specializations.GetAllAsync(cancellationToken);
            logger.LogInformation("GET /api/specializations завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> GetById(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/specializations/{SpecializationId}", async () =>
        {
            logger.LogInformation("GET /api/specializations/{SpecializationId}", id);
            var result = await specializations.GetByIdAsync(id, cancellationToken);
            logger.LogInformation("GET /api/specializations/{SpecializationId} завершён", id);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}/veterinarians")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetVeterinarians(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/specializations/{SpecializationId}/veterinarians", async () =>
        {
            logger.LogInformation("GET /api/specializations/{SpecializationId}/veterinarians", id);
            var result = await veterinarians.GetBySpecializationAsync(id, cancellationToken);
            logger.LogInformation(
                "GET /api/specializations/{SpecializationId}/veterinarians завершён, записей: {Count}",
                id,
                result.Count);
            return Ok(result);

        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Create(SpecializationEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("POST /api/specializations", async () =>
        {
            logger.LogInformation("POST /api/specializations");
            var created = await specializations.CreateAsync(dto, cancellationToken);
            logger.LogInformation("POST /api/specializations завершён, идентификатор: {SpecializationId}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Update(int id, SpecializationEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("PUT /api/specializations/{SpecializationId}", async () =>
        {
            logger.LogInformation("PUT /api/specializations/{SpecializationId}", id);
            var updated = await specializations.UpdateAsync(id, dto, cancellationToken);
            logger.LogInformation("PUT /api/specializations/{SpecializationId} завершён", id);
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
        return await Execute("DELETE /api/specializations/{SpecializationId}", async () =>
        {
            logger.LogInformation("DELETE /api/specializations/{SpecializationId}", id);
            await specializations.DeleteAsync(id, cancellationToken);
            logger.LogInformation("DELETE /api/specializations/{SpecializationId} завершён", id);
            return NoContent();

        });
    }
}
