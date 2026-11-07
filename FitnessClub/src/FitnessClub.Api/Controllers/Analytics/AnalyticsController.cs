using System.ComponentModel.DataAnnotations;
using FitnessClub.Contracts.Dtos.Analytics;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FitnessClub.Api.Controllers.Analytics;

/// <summary>
/// Запросы аналитики фитнес-клуба
/// </summary>
/// <param name="analyticsService">Сервис аналитики фитнес-клуба</param>
[ApiController]
[Route("api/analytics")]
[Produces("application/json")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Получить тренеров с указанным минимальным стажем
    /// </summary>
    /// <param name="minimumExperience">Минимальный стаж тренера в годах, например 5</param>
    /// <returns>Список тренеров, подходящих по минимальному стажу</returns>
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetExperiencedTrainers(
        [FromQuery, Required, Range(0, int.MaxValue)]
        int minimumExperience = 5) =>
        Ok(await analyticsService.GetExperiencedTrainersAsync(minimumExperience));

    /// <summary>
    /// Получить клиентов с просроченным абонементом на указанную дату
    /// </summary>
    /// <param name="asOfDate">Дата проверки срока действия абонемента в формате yyyy-MM-dd, по умолчанию текущая дата</param>
    /// <returns>Список клиентов с просроченными абонементами</returns>
    [HttpGet("overdue-subscriptions")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsWithOverdueSubscriptions(
        [FromQuery]
        DateOnly? asOfDate = null) 
    {
        var subscriptionCheckDate = asOfDate ?? DateOnly.FromDateTime(DateTime.Today);
        return Ok(await analyticsService.GetClientsWithOverdueSubscriptionsAsync(subscriptionCheckDate));
    }

    /// <summary>
    /// Получить тренеров с наибольшим количеством бронирований
    /// </summary>
    /// <param name="count">Максимальное количество тренеров в результате, не меньше 1</param>
    /// <returns>Список самых популярных тренеров</returns>
    [HttpGet("popular-trainers")]
    [ProducesResponseType(typeof(IReadOnlyList<PopularTrainerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PopularTrainerDto>>> GetMostPopularTrainers(
        [FromQuery, Required, Range(1, int.MaxValue)]
        int count = 5) =>
        Ok(await analyticsService.GetMostPopularTrainersAsync(count));

    /// <summary>
    /// Проверить, свободен ли зал в указанный момент времени
    /// </summary>
    /// <param name="hallId">Идентификатор зала, например 5</param>
    /// <param name="dateTime">Дата и время проверки доступности в формате yyyy-MM-ddTHH:mm:ssZ, по умолчанию текущая дата</param>
    /// <returns>Признак доступности зала в указанный момент времени</returns>
    [HttpGet("halls/{hallId:int}/availability")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> IsHallAvailable(
        [FromRoute, Required, Range(1, int.MaxValue)]
        int hallId,
        [FromQuery]
        DateTime? dateTime = null)
    {
        var checkTime  = dateTime ?? DateTime.Now;
        return Ok(await analyticsService.IsHallAvailableAsync(hallId, checkTime ));
    }

    /// <summary>
    /// Получить бронирования зала за месяц указанной даты
    /// </summary>
    /// <param name="hallId">Идентификатор зала, например 5</param>
    /// <param name="dateInMonth">Дата месяца поиска в формате yyyy-MM-dd, по умолчанию текущая дата</param>
    /// <returns>Список бронирований зала за указанный месяц</returns>
    [HttpGet("bookings")]
    [ProducesResponseType(typeof(IReadOnlyList<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetBookingsInHallForMonth(
        [FromQuery, Required, Range(1, int.MaxValue)]
        int hallId,
        [FromQuery]
        DateOnly? dateInMonth = null)
    {
        var monthDate = dateInMonth ?? DateOnly.FromDateTime(DateTime.Today);
        return Ok(await analyticsService.GetBookingsInHallForMonthAsync(hallId, monthDate));
    }
}
