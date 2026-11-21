using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции аналитики фитнес-клуба.
/// </summary>
/// <param name="logger">Журнал ошибок контроллера.</param>
/// <param name="service">Сервис аналитики.</param>
[ApiController]
[Route("api/analytics")]
[Produces("application/json")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
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
        try
        {
            var trainers = await service.GetExperiencedTrainersAsync();

            return Ok(trainers);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
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
        try
        {
            var result = await service.CheckHallAvailabilityAsync(hallId);

            return result is null ? NotFound() : Ok(result);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
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
        try
        {
            var clients =
                await service.GetClientsWithExpiredSubscriptionsAsync();

            return Ok(clients);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }

    /// <summary>
    /// Возвращает тренировки выбранного зала за текущий месяц.
    /// </summary>
    /// <param name="hallId">ID зала.</param>
    [HttpGet("halls/{hallId:int}/current-month-trainings")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(
        typeof(IReadOnlyCollection<TrainingDto>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TrainingDto>>>
        GetCurrentMonthTrainings(int hallId)
    {
        try
        {
            var trainings =
                await service.GetCurrentMonthTrainingsByHallAsync(hallId);

            return trainings is null ? NotFound() : Ok(trainings);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
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
        try
        {
            var trainers = await service.GetTopFiveTrainersAsync();

            return Ok(trainers);
        }
        catch (Exception exception) when (!DatabaseExceptionHandler.IsDatabaseException(exception))
        {
            logger.LogError(exception, "Ошибка при выполнении {Method} {Path}.", Request.Method, Request.Path);
            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                title: "Не удалось выполнить запрос.");
        }
    }
}
