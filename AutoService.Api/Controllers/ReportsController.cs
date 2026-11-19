using AutoService.Contracts.Dtos;
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
    public async Task<ActionResult<IEnumerable<MechanicReportDto>>> GetMechanicsByWork([FromQuery] string workName)
    {
        return Ok(await analyticsService.GetMechanicsByWorkAsync(workName));
    }

    /// <summary>
    /// 2. Получить клиентов указанного механика, упорядоченных по ФИО.
    /// </summary>
    /// <param name="mechanicId">Идентификатор механика.</param>
    [HttpGet("clients-by-mechanic/{mechanicId}")]
    public async Task<ActionResult<IEnumerable<ClientMechanicReportDto>>> GetClientsByMechanic(Guid mechanicId)
    {
        return Ok(await analyticsService.GetClientsByMechanicAsync(mechanicId));
    }

    /// <summary>
    /// 3. Получить клиентов с повторными обращениями за последний месяц.
    /// </summary>
    [HttpGet("repeat-clients")]
    public async Task<ActionResult<IEnumerable<RepeatClientReportDto>>> GetRepeatClients()
    {
        return Ok(await analyticsService.GetRepeatClientsAsync());
    }

    /// <summary>
    /// 4. Подсчитать суммарную стоимость работ для выбранного заказа.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    [HttpGet("order-total/{orderId}")]
    public async Task<ActionResult<OrderTotalReportDto>> GetOrderTotalCost(Guid orderId)
    {
        var total = await analyticsService.GetOrderTotalCostAsync(orderId);
        return Ok(new OrderTotalReportDto
        {
            OrderId = orderId,
            TotalCost = total
        });
    }

    /// <summary>
    /// 5. Получить топ 5 наиболее частых видов работ.
    /// </summary>
    [HttpGet("top-5-works")]
    public async Task<ActionResult<IEnumerable<TopWorkReportDto>>> GetTop5Works()
    {
        return Ok(await analyticsService.GetTop5WorksAsync());
    }
}
