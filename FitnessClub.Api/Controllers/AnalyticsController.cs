using System.ComponentModel.DataAnnotations;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Аналитические запросы
/// </summary>
/// <param name="service">Сервис аналитики</param>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(IAnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Тренеры, стаж работы которых не менее заданного
    /// </summary>
    /// <param name="minYears">Минимальный стаж в годах</param>
    /// <returns>Коллекция тренеров</returns>
    [HttpGet("trainers/experienced")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetExperiencedTrainers(
        [FromQuery, Range(0, 100)] int minYears = 5) =>
        Ok(await service.GetTrainersWithExperienceAsync(minYears));

    /// <summary>
    /// Клиенты с просроченным абонементом
    /// </summary>
    /// <returns>Коллекция клиентов</returns>
    [HttpGet("clients/expired-subscription")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsWithExpiredSubscription() =>
        Ok(await service.GetClientsWithExpiredSubscriptionAsync());

    /// <summary>
    /// Проверяет, доступен ли зал для записи в указанный момент времени
    /// </summary>
    /// <param name="hall">Название зала</param>
    /// <param name="time">Момент времени</param>
    /// <returns>Результат проверки</returns>
    [HttpGet("halls/availability")]
    [ProducesResponseType(typeof(HallAvailabilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<HallAvailabilityDto>> CheckHallAvailability(
        [FromQuery, Required] string hall,
        [FromQuery] DateTime? time) =>
        Ok(await service.CheckHallAvailabilityAsync(hall, time ?? DateTime.Now));

    /// <summary>
    /// Занятия за текущий месяц в выбранном зале
    /// </summary>
    /// <param name="hall">Название зала</param>
    /// <returns>Коллекция занятий</returns>
    [HttpGet("halls/lessons")]
    [ProducesResponseType(typeof(IReadOnlyList<LessonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<LessonDto>>> GetHallLessons(
        [FromQuery, Required] string hall) =>
        Ok(await service.GetHallLessonsForCurrentMonthAsync(hall));

    /// <summary>
    /// Топ наиболее популярных тренеров по количеству занятий
    /// </summary>
    /// <param name="count">Количество тренеров в выборке</param>
    /// <returns>Коллекция популярных тренеров</returns>
    [HttpGet("trainers/top")]
    [ProducesResponseType(typeof(IReadOnlyList<PopularTrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PopularTrainerDto>>> GetTopTrainers(
        [FromQuery, Range(1, 50)] int count = 5) =>
        Ok(await service.GetTopPopularTrainersAsync(count));
}