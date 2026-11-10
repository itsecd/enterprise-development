using System.ComponentModel.DataAnnotations;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Аналитические запросы по данным салона красоты
/// </summary>
/// <param name="service">Сервис аналитики</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AnalyticsController(IAnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Мастера со стажем не менее указанного
    /// </summary>
    /// <param name="minExperience">Минимальный стаж в годах (по умолчанию 5)</param>
    /// <response code="200">Список мастеров</response>
    /// <response code="400">Некорректные параметры</response>
    [HttpGet("experienced-masters")]
    [ProducesResponseType<IReadOnlyList<MasterDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<MasterDto>>> GetExperiencedMasters(
        [FromQuery, Range(0, 80)] int minExperience = 5,
        CancellationToken cancellationToken = default) =>
        Ok(await service.GetExperiencedMastersAsync(minExperience, cancellationToken));

    /// <summary>
    /// Свободные окна мастера на выбранную дату (рабочий день 09:00–18:00)
    /// </summary>
    /// <param name="masterId">Идентификатор мастера</param>
    /// <param name="date">Дата в формате ГГГГ-ММ-ДД</param>
    /// <response code="200">Список свободных окон</response>
    /// <response code="400">Некорректные параметры</response>
    /// <response code="404">Мастер не найден</response>
    [HttpGet("masters/{masterId:int}/free-slots")]
    [ProducesResponseType<IReadOnlyList<TimeSlotDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<TimeSlotDto>>> GetFreeSlots(
        int masterId,
        [FromQuery, Required] DateOnly? date,
        CancellationToken cancellationToken = default) =>
        Ok(await service.GetFreeSlotsAsync(masterId, date!.Value, cancellationToken));

    /// <summary>
    /// Топ наиболее популярных услуг по количеству записей
    /// </summary>
    /// <param name="top">Сколько услуг вернуть (по умолчанию 5)</param>
    /// <response code="200">Список услуг с количеством записей</response>
    /// <response code="400">Некорректные параметры</response>
    [HttpGet("popular-services")]
    [ProducesResponseType<IReadOnlyList<PopularServiceDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<PopularServiceDto>>> GetPopularServices(
        [FromQuery, Range(1, 100)] int top = 5,
        CancellationToken cancellationToken = default) =>
        Ok(await service.GetPopularServicesAsync(top, cancellationToken));

    /// <summary>
    /// Клиенты, записавшиеся более одного раза в выбранном месяце
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц (1–12)</param>
    /// <response code="200">Клиенты с количеством записей</response>
    /// <response code="400">Некорректные параметры</response>
    [HttpGet("repeat-clients")]
    [ProducesResponseType<IReadOnlyList<RepeatClientDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<RepeatClientDto>>> GetRepeatClients(
        [FromQuery, Range(1, 9999)] int year,
        [FromQuery, Range(1, 12)] int month,
        CancellationToken cancellationToken = default) =>
        Ok(await service.GetRepeatClientsAsync(year, month, cancellationToken));

    /// <summary>
    /// Клиенты, записанные к нескольким мастерам, упорядоченные по дате рождения
    /// </summary>
    /// <response code="200">Список клиентов</response>
    [HttpGet("clients-with-multiple-masters")]
    [ProducesResponseType<IReadOnlyList<ClientDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsWithMultipleMasters(
        CancellationToken cancellationToken = default) =>
        Ok(await service.GetClientsWithMultipleMastersAsync(cancellationToken));
}