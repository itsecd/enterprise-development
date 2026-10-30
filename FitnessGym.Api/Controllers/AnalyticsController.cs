using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// Выполняет аналитические запросы по данным фитнес-клуба
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Тренеры со стажем не менее заданного
    /// </summary>
    /// <param name="minExperience">Минимальный стаж (по умолчанию 5 лет)</param>
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetExperiencedTrainers([FromQuery] int minExperience = 5)
    {
        try
        {
            var trainers = await service.GetExperiencedTrainersAsync(minExperience);
            return Ok(trainers);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetExperiencedTrainers));
        }
    }

    /// <summary>
    /// Проверяет доступность зала в указанный момент
    /// </summary>
    /// <param name="hallName">Название зала</param>
    /// <param name="moment">Момент времени</param>
    [HttpGet("hall-availability")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> IsHallAvailable([FromQuery] string hallName, [FromQuery] DateTime moment)
    {
        try
        {
            var available = await service.IsHallAvailableAsync(hallName, moment);
            return Ok(available);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(IsHallAvailable));
        }
    }

    /// <summary>
    /// Клиенты с просроченным абонементом, упорядоченные по ФИО
    /// </summary>
    /// <param name="currentDate">Дата проверки (по умолчанию сегодня)</param>
    [HttpGet("expired-subscriptions")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetExpiredSubscriptions([FromQuery] DateTime? currentDate = null)
    {
        try
        {
            var date = currentDate ?? DateTime.UtcNow;
            var clients = await service.GetClientsWithExpiredSubscriptionAsync(date);
            return Ok(clients);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetExpiredSubscriptions));
        }
    }

    /// <summary>
    /// Занятия за указанный месяц в выбранном зале
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц</param>
    /// <param name="hallName">Название зала</param>
    [HttpGet("sessions-by-hall")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainingSessionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainingSessionDto>>> GetSessionsForMonthInHall(
        [FromQuery] int year, [FromQuery] int month, [FromQuery] string hallName)
    {
        try
        {
            var sessions = await service.GetSessionsForMonthInHallAsync(year, month, hallName);
            return Ok(sessions);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetSessionsForMonthInHall));
        }
    }

    /// <summary>
    /// Топ-5 наиболее популярных тренеров
    /// </summary>
    [HttpGet("top-5-trainers")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetTop5PopularTrainers()
    {
        try
        {
            var trainers = await service.GetTop5PopularTrainersAsync();
            return Ok(trainers);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetTop5PopularTrainers));
        }
    }

    private ObjectResult ServerError(Exception exception, string operation)
    {
        logger.LogError(exception, "Ошибка при выполнении операции {Operation}", operation);
        return Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Не удалось обработать запрос.");
    }
}