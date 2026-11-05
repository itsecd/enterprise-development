using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Breeds;

namespace VetClinic.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/breeds")]
public class BreedsController(IBreedService breeds, ILogger<BreedsController> logger) : ClinicControllerBase(logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BreedDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<BreedDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await Execute("GET /api/breeds", async () =>
        {
            logger.LogInformation("GET /api/breeds");
            var result = await breeds.GetAllAsync(cancellationToken);
            logger.LogInformation("GET /api/breeds завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BreedDto>> GetById(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/breeds/{BreedId}", async () =>
        {
            logger.LogInformation("GET /api/breeds/{BreedId}", id);
            var result = await breeds.GetByIdAsync(id, cancellationToken);
            logger.LogInformation("GET /api/breeds/{BreedId} завершён", id);
            return Ok(result);

        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BreedDto>> Create(BreedEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("POST /api/breeds", async () =>
        {
            logger.LogInformation("POST /api/breeds");
            var created = await breeds.CreateAsync(dto, cancellationToken);
            logger.LogInformation("POST /api/breeds завершён, идентификатор: {BreedId}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BreedDto>> Update(int id, BreedEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("PUT /api/breeds/{BreedId}", async () =>
        {
            logger.LogInformation("PUT /api/breeds/{BreedId}", id);
            var updated = await breeds.UpdateAsync(id, dto, cancellationToken);
            logger.LogInformation("PUT /api/breeds/{BreedId} завершён", id);
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
        return await Execute("DELETE /api/breeds/{BreedId}", async () =>
        {
            logger.LogInformation("DELETE /api/breeds/{BreedId}", id);
            await breeds.DeleteAsync(id, cancellationToken);
            logger.LogInformation("DELETE /api/breeds/{BreedId} завершён", id);
            return NoContent();

        });
    }
}
