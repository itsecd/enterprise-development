using DryCleaner.Application.Contracts.Analytic;
using DryCleaner.Application.Contracts.Order;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для аналитических запросов к данным сервиса.
/// </summary>
/// <param name="service">Сервис аналитических запросов.</param>
/// <param name="logger">Логгер для записи событий контроллера.</param>
[Route("api/[controller]")]
[ApiController]
public class AnalyticController(IAnalyticService service, ILogger<AnalyticController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает заказы, находящиеся в обработке, упорядоченные по дате приема.
    /// </summary>
    /// <returns>Код состояния 200 и список заказов; код состояния 500 в случае ошибки сервера.</returns>
    [HttpGet("in-progress-orders")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<OrderDto>>> GetInProgressOrders()
    {
        logger.LogInformation("{method} method of {Pcontroller} is called.",
            nameof(GetInProgressOrders), GetType().Name);

        try
        {
            var result = await service.GetInProgressOrders();
            logger.LogInformation("{method} method of {Pcontroller} was called. Result: {result}",
                nameof(GetInProgressOrders), GetType().Name, result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called. Ended with an error: {errorText}",
                nameof(GetInProgressOrders), GetType().Name, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает топ 5 самых активных клиентов за период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Код состояния 200 и список клиентов с количеством заказов; код состояния 500 в случае ошибки сервера.</returns>
    [HttpGet("top-active-clients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ClientOrdersCountDto>>> GetTop5MostActiveClients(DateTime dateFrom, DateTime dateTo)
    {
        logger.LogInformation("{method} method of {Pcontroller} is called with parameters: dateFrom={dateFrom}, dateTo={dateTo}.",
            nameof(GetTop5MostActiveClients), GetType().Name, dateFrom, dateTo);

        try
        {
            var result = await service.GetTop5MostActiveClients(dateFrom, dateTo);
            logger.LogInformation("{method} method of {Pcontroller} was called with parameters: dateFrom={dateFrom}, dateTo={dateTo}. Result: {result}",
                nameof(GetTop5MostActiveClients), GetType().Name, dateFrom, dateTo, result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called with parameters: dateFrom={dateFrom}, dateTo={dateTo}. Ended with an error: {errorText}",
                nameof(GetTop5MostActiveClients), GetType().Name, dateFrom, dateTo, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает клиентов со средним сроком выполнения их заказов, упорядоченных по имени.
    /// </summary>
    /// <returns>Код состояния 200 и список клиентов со средним сроком выполнения заказов; код состояния 500 в случае ошибки сервера.</returns>
    [HttpGet("clients-average-processing-days")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ClientAverageProcessingDaysDto>>> GetClientsAverageProcessingDays()
    {
        logger.LogInformation("{method} method of {Pcontroller} is called.",
            nameof(GetClientsAverageProcessingDays), GetType().Name);

        try
        {
            var result = await service.GetClientsAverageProcessingDays();
            logger.LogInformation("{method} method of {Pcontroller} was called. Result: {result}",
                nameof(GetClientsAverageProcessingDays), GetType().Name, result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called. Ended with an error: {errorText}",
                nameof(GetClientsAverageProcessingDays), GetType().Name, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }
    }


    /// <summary>
    /// Возвращает 5 самых популярных категорий изделий за указанный период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Список самых популярных категорий</returns>
    [HttpGet("categories-most-popularity")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CategoriesPopularityDto>> GetTop5MostPopularCategories(DateTime dateFrom, DateTime dateTo)
    {
        logger.LogInformation("{method} method of {Pcontroller} is called with parameters: dateFrom={dateFrom}, dateTo={dateTo}.",
            nameof(GetTop5MostPopularCategories), GetType().Name, dateFrom, dateTo);

        try
        {
            var result = await service.GetTop5MostPopularCategories(dateFrom, dateTo);
            logger.LogInformation("{method} method of {Pcontroller} was called with parameters: dateFrom={dateFrom}, dateTo={dateTo}. Result: {result}",
                nameof(GetTop5MostPopularCategories), GetType().Name, dateFrom, dateTo, result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called with parameters: dateFrom={dateFrom}, dateTo={dateTo}. Ended with an error: {errorText}",
                nameof(GetTop5MostPopularCategories), GetType().Name, dateFrom, dateTo, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }
    }


    /// <summary>
    /// Возвращает 5 самых не популярных категорий изделий за указанный период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Список самых не популярных категорий</returns>
    [HttpGet("categories-least-popularity")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CategoriesPopularityDto>> GetTop5LeastPopularCategories(DateTime dateFrom, DateTime dateTo)
    {
        logger.LogInformation("{method} method of {Pcontroller} is called with parameters: dateFrom={dateFrom}, dateTo={dateTo}.",
            nameof(GetTop5LeastPopularCategories), GetType().Name, dateFrom, dateTo);

        try
        {
            var result = await service.GetTop5LeastPopularCategories(dateFrom, dateTo);
            logger.LogInformation("{method} method of {Pcontroller} was called with parameters: dateFrom={dateFrom}, dateTo={dateTo}. Result: {result}",
                nameof(GetTop5LeastPopularCategories), GetType().Name, dateFrom, dateTo, result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called with parameters: dateFrom={dateFrom}, dateTo={dateTo}. Ended with an error: {errorText}",
                nameof(GetTop5LeastPopularCategories), GetType().Name, dateFrom, dateTo, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает клиента, потратившего самую большую сумму за весь период работы сервиса.
    /// </summary>
    /// <returns>Код состояния 200 и клиент с потраченной суммой;Код состояния 200 и пустой массив, если заказов нет; 500 в случае ошибки сервера.</returns>
    [HttpGet("top-spending-client")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ClientTotalSpentDto>>> GetClientWhoSpentTheMostMoney()
    {
        logger.LogInformation("{method} method of {Pcontroller} is called.",
            nameof(GetClientWhoSpentTheMostMoney), GetType().Name);

        try
        {
            var result = await service.GetClientWhoSpentTheMostMoney();
            logger.LogInformation("{method} method of {Pcontroller} was called. Result: {result}",
                nameof(GetClientWhoSpentTheMostMoney), GetType().Name, result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called. Ended with an error: {errorText}",
                nameof(GetClientWhoSpentTheMostMoney), GetType().Name, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }
    }
}
