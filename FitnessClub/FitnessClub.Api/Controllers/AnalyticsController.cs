using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции аналитики фитнес-клуба.
/// </summary>
/// <param name="service">Сервис аналитики.</param>
[ApiController]
[Route("api/analytics")]
[Produces("application/json")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class AnalyticsController(IAnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Возвращает тренеров со стажем от пяти лет.
    /// </summary>
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(
        typeof(IReadOnlyCollection<TrainerDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TrainerDto>>>
        GetExperiencedTrainers()
    {
        var trainers = await service.GetExperiencedTrainersAsync();

        return Ok(trainers);
    }

    /// <summary>
    /// Проверяет, свободен ли зал прямо сейчас.
    /// </summary>
    /// <param name="hallId">ID зала.</param>
    [HttpGet("halls/{hallId:int}/availability")]
    [ProducesResponseType(
        typeof(HallAvailabilityDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HallAvailabilityDto>> CheckHallAvailability(
        int hallId)
    {
        var result = await service.CheckHallAvailabilityAsync(hallId);

        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Возвращает клиентов с истёкшими абонементами.
    /// </summary>
    [HttpGet("expired-subscriptions")]
    [ProducesResponseType(
        typeof(IReadOnlyCollection<ClientDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<ClientDto>>>
        GetExpiredSubscriptions()
    {
        var clients =
            await service.GetClientsWithExpiredSubscriptionsAsync();

        return Ok(clients);
    }

    /// <summary>
    /// Возвращает тренировки выбранного зала за текущий месяц.
    /// </summary>
    /// <param name="hallId">ID зала.</param>
    [HttpGet("halls/{hallId:int}/current-month-trainings")]
    [ProducesResponseType(
        typeof(IReadOnlyCollection<TrainingDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TrainingDto>>>
        GetCurrentMonthTrainings(int hallId)
    {
        var trainings =
            await service.GetCurrentMonthTrainingsByHallAsync(hallId);

        return Ok(trainings);
    }

    /// <summary>
    /// Возвращает пять самых популярных тренеров.
    /// </summary>
    [HttpGet("top-trainers")]
    [ProducesResponseType(
        typeof(IReadOnlyCollection<TrainerDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TrainerDto>>>
        GetTopTrainers()
    {
        var trainers = await service.GetTopFiveTrainersAsync();

        return Ok(trainers);
    }
}