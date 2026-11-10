using Microsoft.AspNetCore.Mvc;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;

namespace TransportCompany.Api.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticController(IAnalyticsService analyticsService, ILogger<AnalyticController> logger) : ControllerBase
{
    private readonly IAnalyticsService _analyticsService = analyticsService;
    private readonly ILogger<AnalyticController> _logger = logger;

    /// <summary>
    /// Вывести информацию обо всех клиентах, заказывавших перевозку на транспорте указанной модели
    /// </summary>
    [HttpGet("clients-by-model/{modelId:int}")]
    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClientsByModel(int modelId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _analyticsService.GetClientsByVehicleModelAsync(modelId, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении аналитики: клиенты по модели");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Вывести информацию о транспортных средствах, находящихся в рейсе
    /// </summary>
    [HttpGet("vehicles-in-transit")]
    [ProducesResponseType(typeof(IEnumerable<VehicleDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehiclesInTransit(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _analyticsService.GetVehiclesInTransitAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении аналитики: ТС в рейсе");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Вывести топ 5 водителей по количеству рейсов
    /// </summary>
    [HttpGet("top-drivers")]
    [ProducesResponseType(typeof(IEnumerable<TopDriverDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<TopDriverDto>>> GetTop5Drivers(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _analyticsService.GetTop5DriversAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении аналитики: топ водителей");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Для каждого транспортного средства вывести число рейсов
    /// </summary>
    [HttpGet("vehicle-trip-counts")]
    [ProducesResponseType(typeof(IEnumerable<VehicleTripCountDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<VehicleTripCountDto>>> GetTripCounts(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _analyticsService.GetTripCountForVehiclesAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении аналитики: рейсы по ТС");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Вывести топ 5 клиентов по сумме затрат на перевозки
    /// </summary>
    [HttpGet("top-clients-by-cost")]
    [ProducesResponseType(typeof(IEnumerable<TopClientDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<TopClientDto>>> GetTopClientsByCost(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _analyticsService.GetTop5ClientsByCostAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении аналитики: топ клиентов по затратам");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}