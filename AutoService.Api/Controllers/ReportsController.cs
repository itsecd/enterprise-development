using AutoService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Api.Controllers;

/// <summary>
/// Контроллер для аналитических запросов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReportsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// 1. Получить механиков, специализирующихся на выбранном виде работ.
    /// </summary>
    /// <param name="workName">Название работы (например, "Диагностика ходовой").</param>
    [HttpGet("mechanics-by-work")]
    public async Task<IActionResult> GetMechanicsByWork([FromQuery] string workName)
    {
        try
        {
            var result = await analyticsService.GetMechanicsByWorkAsync(workName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// 2. Получить клиентов указанного механика, упорядоченных по ФИО.
    /// </summary>
    /// <param name="mechanicId">Идентификатор механика.</param>
    [HttpGet("clients-by-mechanic/{mechanicId}")]
    public async Task<IActionResult> GetClientsByMechanic(Guid mechanicId)
    {
        try
        {
            var result = await analyticsService.GetClientsByMechanicAsync(mechanicId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// 3. Получить клиентов с повторными обращениями за последний месяц.
    /// </summary>
    [HttpGet("repeat-clients")]
    public async Task<IActionResult> GetRepeatClients()
    {
        var result = await analyticsService.GetRepeatClientsAsync();
        return Ok(result);
    }

    /// <summary>
    /// 4. Подсчитать суммарную стоимость работ для выбранного заказа.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    [HttpGet("order-total/{orderId}")]
    public async Task<IActionResult> GetOrderTotalCost(Guid orderId)
    {
        try
        {
            var total = await analyticsService.GetOrderTotalCostAsync(orderId);
            return Ok(new { OrderId = orderId, TotalCost = total });
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// 5. Получить топ 5 наиболее частых видов работ.
    /// </summary>
    [HttpGet("top-5-works")]
    public async Task<IActionResult> GetTop5Works()
    {
        var result = await analyticsService.GetTop5WorksAsync();
        return Ok(result);
    }
}