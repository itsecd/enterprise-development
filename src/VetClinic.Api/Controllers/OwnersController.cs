using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Owners;
using VetClinic.Contracts.Pets;

namespace VetClinic.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/owners")]
public class OwnersController(IOwnerService owners, IPetService pets, ILogger<OwnersController> logger) : ClinicControllerBase(logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await Execute("GET /api/owners", async () =>
        {
            logger.LogInformation("GET /api/owners");
            var result = await owners.GetAllAsync(cancellationToken);
            logger.LogInformation("GET /api/owners завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("with-several-pets")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> GetWithSeveralPets(CancellationToken cancellationToken)
    {
        return await Execute("GET /api/owners/with-several-pets", async () =>
        {
            logger.LogInformation("GET /api/owners/with-several-pets");
            var result = await owners.GetWithSeveralPetsAsync(cancellationToken);
            logger.LogInformation("GET /api/owners/with-several-pets завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OwnerDto>> GetById(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/owners/{OwnerId}", async () =>
        {
            logger.LogInformation("GET /api/owners/{OwnerId}", id);
            var result = await owners.GetByIdAsync(id, cancellationToken);
            logger.LogInformation("GET /api/owners/{OwnerId} завершён", id);
            return Ok(result);

        });
    }

    [HttpGet("{id:int}/pets")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPets(int id, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/owners/{OwnerId}/pets", async () =>
        {
            logger.LogInformation("GET /api/owners/{OwnerId}/pets", id);
            var result = await pets.GetByOwnerAsync(id, cancellationToken);
            logger.LogInformation("GET /api/owners/{OwnerId}/pets завершён, записей: {Count}", id, result.Count);
            return Ok(result);

        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OwnerDto>> Create(OwnerEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("POST /api/owners", async () =>
        {
            logger.LogInformation("POST /api/owners");
            var created = await owners.CreateAsync(dto, cancellationToken);
            logger.LogInformation("POST /api/owners завершён, идентификатор: {OwnerId}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OwnerDto>> Update(int id, OwnerEditDto dto, CancellationToken cancellationToken)
    {
        return await Execute("PUT /api/owners/{OwnerId}", async () =>
        {
            logger.LogInformation("PUT /api/owners/{OwnerId}", id);
            var updated = await owners.UpdateAsync(id, dto, cancellationToken);
            logger.LogInformation("PUT /api/owners/{OwnerId} завершён", id);
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
        return await Execute("DELETE /api/owners/{OwnerId}", async () =>
        {
            logger.LogInformation("DELETE /api/owners/{OwnerId}", id);
            await owners.DeleteAsync(id, cancellationToken);
            logger.LogInformation("DELETE /api/owners/{OwnerId} завершён", id);
            return NoContent();

        });
    }
}
