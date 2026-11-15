using AutoServiceLab.Application.Contracts.Dtos.Clients;
using AutoServiceLab.Application.Contracts.Dtos.Mechanics;
using AutoServiceLab.Application.Contracts.Dtos.WorkTypes;
using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Механики по категории работ
    /// </summary>
    [HttpGet("mechanics-by-category")]
    [ProducesResponseType(typeof(IList<MechanicDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetMechanicsByCategory([FromQuery] MechanicSpecialization category)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: механики по категории {Category}", category);
            return Ok(service.GetMechanicsByWorkCategory(category));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении механиков по категории {Category}", category);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Клиенты механика
    /// </summary>
    [HttpGet("clients-by-mechanic/{id}")]
    [ProducesResponseType(typeof(IList<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetClientsByMechanic(int id)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: клиенты механика {Id}", id);
            return Ok(service.GetClientsByMechanic(id));
        }
        catch (KeyNotFoundException ex) 
        {
            logger.LogWarning(ex, "Механик с ID {Id} не найден", id);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиентов механика {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Повторные обращения за последний месяц
    /// </summary>
    [HttpGet("repeat-clients")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetRepeatClients()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: количество повторных клиентов");
            return Ok(service.GetRepeatClientsCount());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении количества повторных клиентов");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Стоимость работ заказа
    /// </summary>
    [HttpGet("order-cost/{id}")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetOrderCost(int id)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: стоимость заказа {Id}", id);
            return Ok(service.GetOrderTotalCost(id));
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Заказ с ID {Id} не найден", id);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении стоимости заказа {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Топ-5 видов работ
    /// </summary>
    [HttpGet("top-worktypes")]
    [ProducesResponseType(typeof(IList<WorkTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetTopWorkTypes()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: топ-5 видов работ");
            return Ok(service.GetTopFiveWorkTypes());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топа видов работ");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера" });
        }
    }
}
