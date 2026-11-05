using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Appointments;
using VetClinic.Contracts.Owners;
using VetClinic.Contracts.Pets;
using VetClinic.Contracts.Veterinarians;
using VetClinic.Domain.Enums;

namespace VetClinic.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/analytics")]
public class AnalyticsController(
    IVeterinarianService veterinarians,
    IPetService pets,
    IOwnerService owners,
    IAppointmentService appointments,
    ILogger<AnalyticsController> logger) : ClinicControllerBase(logger)
{
    [HttpGet("veterinarians")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> Veterinarians([FromQuery] AnimalSpecies? species, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/analytics/veterinarians, вид: {Species}", async () =>
        {
            logger.LogInformation("GET /api/analytics/veterinarians, вид: {Species}", species);
            if (species is not { } selected || !Enum.IsDefined(selected))
            {
                return BadRequest("Укажите биологический вид.");
            }

            var result = await veterinarians.GetBySpeciesAsync(selected, cancellationToken);
            logger.LogInformation("GET /api/analytics/veterinarians завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("veterinarians/{veterinarianId:int}/pets")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> PetsOfVeterinarian(int veterinarianId, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/analytics/veterinarians/{VeterinarianId}/pets", async () =>
        {
            logger.LogInformation("GET /api/analytics/veterinarians/{VeterinarianId}/pets", veterinarianId);
            var result = await pets.GetByVeterinarianAsync(veterinarianId, cancellationToken);
            logger.LogInformation(
                "GET /api/analytics/veterinarians/{VeterinarianId}/pets завершён, записей: {Count}",
                veterinarianId,
                result.Count);
            return Ok(result);

        });
    }

    [HttpGet("breeds/{breedId:int}/follow-ups")]
    [ProducesResponseType(typeof(FollowUpCountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FollowUpCountDto>> FollowUps(int breedId, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/analytics/breeds/{BreedId}/follow-ups", async () =>
        {
            logger.LogInformation("GET /api/analytics/breeds/{BreedId}/follow-ups", breedId);
            var result = await appointments.CountFollowUpsAsync(breedId, cancellationToken);
            logger.LogInformation("GET /api/analytics/breeds/{BreedId}/follow-ups завершён, количество: {Count}", breedId, result.Count);
            return Ok(result);

        });
    }

    [HttpGet("owners/with-several-pets")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> OwnersWithSeveralPets(CancellationToken cancellationToken)
    {
        return await Execute("GET /api/analytics/owners/with-several-pets", async () =>
        {
            logger.LogInformation("GET /api/analytics/owners/with-several-pets");
            var result = await owners.GetWithSeveralPetsAsync(cancellationToken);
            logger.LogInformation("GET /api/analytics/owners/with-several-pets завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }

    [HttpGet("appointments")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> CurrentMonthAppointments([FromQuery] string? roomNumber, CancellationToken cancellationToken)
    {
        return await Execute("GET /api/analytics/appointments, кабинет: {RoomNumber}", async () =>
        {
            logger.LogInformation("GET /api/analytics/appointments, кабинет: {RoomNumber}", roomNumber);
            if (string.IsNullOrWhiteSpace(roomNumber))
            {
                return BadRequest("Укажите номер кабинета.");
            }

            var result = await appointments.GetCurrentMonthAsync(roomNumber, cancellationToken);
            logger.LogInformation("GET /api/analytics/appointments завершён, записей: {Count}", result.Count);
            return Ok(result);

        });
    }
}
