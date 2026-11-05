using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Breeds;
using VetClinic.Contracts.Specializations;
using VetClinic.Contracts.Veterinarians;
using VetClinic.Domain.Enums;

namespace VetClinic.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/species/{species}")]
public class SpeciesController(
    IBreedService breeds,
    ISpecializationService specializations,
    IVeterinarianService veterinarians,
    ILogger<SpeciesController> logger) : ClinicControllerBase(logger)
{
    [HttpGet("breeds")]
    [ProducesResponseType(typeof(IReadOnlyList<BreedDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<BreedDto>>> Breeds(AnimalSpecies species, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/species/{Species}/breeds", async () =>
        {
            logger.LogInformation("GET /api/species/{Species}/breeds", species);
            if (!Enum.IsDefined(species))
            {
                return BadRequest("Укажите биологический вид.");
            }

            var result = await breeds.GetBySpeciesAsync(species, cancellationToken);
            logger.LogInformation("GET /api/species/{Species}/breeds завершён, записей: {Count}", species, result.Count);
            return Ok(result);

        });
    }

    [HttpGet("specializations")]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> Specializations(AnimalSpecies species, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/species/{Species}/specializations", async () =>
        {
            logger.LogInformation("GET /api/species/{Species}/specializations", species);
            if (!Enum.IsDefined(species))
            {
                return BadRequest("Укажите биологический вид.");
            }

            var result = await specializations.GetBySpeciesAsync(species, cancellationToken);
            logger.LogInformation(
                "GET /api/species/{Species}/specializations завершён, записей: {Count}",
                species,
                result.Count);
            return Ok(result);

        });
    }

    [HttpGet("veterinarians")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> Veterinarians(AnimalSpecies species, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/species/{Species}/veterinarians", async () =>
        {
            logger.LogInformation("GET /api/species/{Species}/veterinarians", species);
            if (!Enum.IsDefined(species))
            {
                return BadRequest("Укажите биологический вид.");
            }

            var result = await veterinarians.GetBySpeciesAsync(species, cancellationToken);
            logger.LogInformation(
                "GET /api/species/{Species}/veterinarians завершён, записей: {Count}",
                species,
                result.Count);
            return Ok(result);

        });
    }
}
