using System.ComponentModel;
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
/// <param name="analyticsService">
/// Сервис аналитики фитнес-клуба
/// </param>
[ApiController]
[Route("api/analytics")]
[Produces("application/json")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Получить тренеров с указанным минимальным стажем
    /// </summary>
    /// <param name="minimumExperience">
    /// Минимальный стаж тренера в годах
    /// </param>
    /// <returns>
    /// Список тренеров, подходящих по минимальному стажу
    /// </returns>
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetExperiencedTrainers(
        [FromQuery, Required, Range(0, int.MaxValue)]
        [Description("Минимальный стаж в годах. Пример: 5.")]
        int minimumExperience = 5) =>
        Ok(await analyticsService.GetExperiencedTrainersAsync(minimumExperience));

    /// <summary>
    /// Получить клиентов с просроченным абонементом на указанную дату
    /// </summary>
    /// <param name="asOfDate">
    /// Дата проверки срока действия абонемента
    /// </param>
    /// <returns>
    /// Список клиентов с просроченными абонементами
    /// </returns>
    [HttpGet("overdue-subscriptions")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsWithOverdueSubscriptions(
        [FromQuery, BindRequired] // или не писать Required я не знаю Т_Т
        [Description("Дата проверки в формате yyyy-MM-dd, без кавычек. Пример: 2026-09-15.")]
        DateOnly asOfDate) =>
        Ok(await analyticsService.GetClientsWithOverdueSubscriptionsAsync(asOfDate));

    /// <summary>
    /// Получить тренеров с наибольшим количеством бронирований
    /// </summary>
    /// <param name="count">
    /// Максимальное количество тренеров в результате
    /// </param>
    /// <returns>
    /// Список самых популярных тренеров
    /// </returns>
    [HttpGet("popular-trainers")]
    [ProducesResponseType(typeof(IReadOnlyList<PopularTrainerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PopularTrainerDto>>> GetMostPopularTrainers(
        [FromQuery, Required, Range(1, int.MaxValue)]
        [Description("Количество тренеров в результате, не меньше 1. Пример: 5.")]
        int count = 5) =>
        Ok(await analyticsService.GetMostPopularTrainersAsync(count));

    /// <summary>
    /// Проверить, свободен ли зал в указанный момент времени
    /// </summary>
    /// <param name="hallId">
    /// Идентификатор зала
    /// </param>
    /// <param name="dateTime">
    /// Дата и время проверки доступности
    /// </param>
    /// <returns>
    /// Признак доступности зала в указанный момент времени
    /// </returns>
    [HttpGet("halls/{hallId:int}/availability")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> IsHallAvailable(
        [FromRoute, Required, Range(1, int.MaxValue)]
        [Description("Идентификатор зала. Пример: 5.")]
        int hallId,
        [FromQuery, BindRequired]
        [Description("Дата и время в формате yyyy-MM-ddTHH:mm:ssZ, без кавычек. Пример: 2026-09-15T10:30:00Z.")]
        DateTime dateTime) =>
        Ok(await analyticsService.IsHallAvailableAsync(hallId, dateTime));

    /// <summary>
    /// Получить бронирования зала за месяц указанной даты
    /// </summary>
    /// <param name="hallId">
    /// Идентификатор зала
    /// </param>
    /// <param name="dateInMonth">
    /// Дата месяца поиска
    /// </param>
    /// <returns>
    /// Список бронирований зала за указанный месяц
    /// </returns>
    [HttpGet("bookings")]
    [ProducesResponseType(typeof(IReadOnlyList<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetBookingsInHallForMonth(
        [FromQuery, Required, Range(1, int.MaxValue)]
        [Description("Идентификатор зала. Пример: 5.")]
        int hallId,
        [FromQuery, BindRequired]
        [Description("Дата месяца в формате yyyy-MM-dd, без кавычек и времени. Пример: 2026-09-01.")]
        DateOnly dateInMonth) =>
        Ok(await analyticsService.GetBookingsInHallForMonthAsync(hallId, dateInMonth));
}
