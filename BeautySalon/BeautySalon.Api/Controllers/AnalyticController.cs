using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Analytics;
using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Application.Contracts.Masters;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
/// <param name="analyticService">Служба аналитики</param>
/// <param name="logger">Логгер</param>
[Route("api/[controller]")]
[ApiController]
public class AnalyticController(IAnalyticService analyticService, ILogger<AnalyticController> logger) : ControllerBase
{
    /// <summary>
    /// Получение списка мастеров со стажем работы не менее 5 лет
    /// </summary>
    /// <returns>Список мастеров</returns>
    [HttpGet("masters-with-experience-at-least-5-years")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<MasterDto>>> GetMastersWithExperienceAtLeastFiveYears()
    {
        try
        {
            return Ok(await analyticService.GetMastersWithExperienceAtLeastFiveYears());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting experienced masters.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение окошек выбранного мастера
    /// </summary>
    /// <param name="masterId">Id мастера</param>
    /// <param name="date">Дата</param>
    /// <returns>Список окошек</returns>
    [HttpGet("masters-free-slots/{masterId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<FreeSlotDto>>> GetFreeSlotsForSelectedMaster(int masterId, DateOnly? date)
    {
        try
        {
            var result = await analyticService.GetFreeSlotsForSelectedMaster(masterId, date);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting free slots of master {MasterId}.", masterId);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение топ 5 популярных услуг
    /// </summary>
    /// <returns>Список услуг с количеством записей</returns>
    [HttpGet("services-top-5")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<PopularServiceDto>>> GetTopFivePopularServices()
    {
        try
        {
            return Ok(await analyticService.GetTopFivePopularServices());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting top five services.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение количества повторных записей клиентов за последний месяц
    /// </summary>
    /// <returns>Количество записей</returns>
    [HttpGet("appointments-repeat-last-month")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<int>> GetNumberOfRepeatClientAppointmentsForLastMonth()
    {
        try
        {
            return Ok(await analyticService.GetNumberOfRepeatClientAppointmentsForLastMonth());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting number of repeat appointments.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Клиенты, записанные к нескольким мастерам (по дате рождения)
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet("clients-several-masters")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ClientDto>>> GetClientsRegisteredWithSeveralMasters()
    {
        try
        {
            return Ok(await analyticService.GetClientsRegisteredWithSeveralMasters());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting clients with several masters.");
            return StatusCode(500, "Internal server error");
        }
    }
}
