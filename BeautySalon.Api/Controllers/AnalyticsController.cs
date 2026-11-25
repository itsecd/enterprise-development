using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Analytics;
using BeautySalon.Contracts.Customers;
using BeautySalon.Contracts.Specialists;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Аналитика салона красоты
/// </summary>
/// <param name="service">Сервис аналитических запросов</param>
[ApiController]
[Route("api/analytics")]
[Produces("application/json")]
public sealed class AnalyticsController(IAnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Получить специалистов со стажем не менее пяти лет
    /// </summary>
    [HttpGet("experienced-specialists")]
    [ProducesResponseType(typeof(IReadOnlyList<SpecialistResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SpecialistResponse>>> GetExperiencedSpecialists(CancellationToken cancellationToken)
    {
        return Ok(await service.GetExperiencedSpecialistsAsync(cancellationToken));
    }

    /// <summary>
    /// Получить свободные промежутки специалиста за день
    /// </summary>
    /// <param name="specialistId">Идентификатор специалиста</param>
    /// <param name="date">День расписания</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("specialists/{specialistId:int}/free-windows")]
    [ProducesResponseType(typeof(IReadOnlyList<FreeWindowResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<FreeWindowResponse>>> GetFreeWindows(
        int specialistId, [FromQuery] DateOnly date, CancellationToken cancellationToken)
    {
        return Ok(await service.GetFreeWindowsAsync(specialistId, date, cancellationToken));
    }

    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    [HttpGet("popular-services")]
    [ProducesResponseType(typeof(IReadOnlyList<PopularServiceResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PopularServiceResponse>>> GetPopularServices(CancellationToken cancellationToken)
    {
        return Ok(await service.GetPopularServicesAsync(cancellationToken));
    }

    /// <summary>
    /// Получить количество повторных записей за последний месяц
    /// </summary>
    /// <param name="asOf">Конец периода или текущее время при отсутствии параметра</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("repeat-appointments")]
    [ProducesResponseType(typeof(RepeatAppointmentsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RepeatAppointmentsResponse>> GetRepeatAppointments(
        [FromQuery] DateTime? asOf, CancellationToken cancellationToken)
    {
        return Ok(await service.GetRepeatAppointmentsAsync(asOf ?? DateTime.Now, cancellationToken));
    }

    /// <summary>
    /// Получить посетителей нескольких специалистов по дате рождения
    /// </summary>
    [HttpGet("customers-with-multiple-specialists")]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerResponse>>> GetCustomersWithMultipleSpecialists(CancellationToken cancellationToken)
    {
        return Ok(await service.GetCustomersWithMultipleSpecialistsAsync(cancellationToken));
    }
}
