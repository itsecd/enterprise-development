using Microsoft.AspNetCore.Mvc;
using DryCleaning.Infrastructure.Repositories;
using DryCleaning.Domain.Models;
using DryCleaning.Api.Dtos;
using DryCleaning.Domain.Data;

namespace DryCleaning.Api.Controllers;

/// <summary>
/// Контроллер аналитических выборок
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IOrderRepository orderRepository, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Запрос 1: Заказы в обработке, упорядоченные по дате приема
    /// </summary>
    [HttpGet("orders-in-progress")]
    public ActionResult<IEnumerable<OrderDto>> GetOrdersInProgress()
    {
        logger.LogInformation("Запрос аналитики: заказы в обработке");

        var result = orderRepository.GetAll()
            .Where(o => o.Status == Status.InProgress)
            .OrderBy(o => o.AcceptDate)
            .Select(o => new OrderDto(o.Id, o.ClientId, o.Client.FullName, o.ItemId, o.Item.Name, o.AcceptDate, o.ExeDays, o.Status))
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Запрос 2: Топ-5 клиентов, сдавших больше всего изделий за заданный период
    /// </summary>
    [HttpGet("top-clients")]
    public ActionResult<IEnumerable<TopClientDto>> GetTopClients([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var from = startDate ?? DataSeed.BaseDate.AddDays(-30);
        var to = endDate ?? DataSeed.BaseDate;

        logger.LogInformation("Запрос аналитики: топ-5 клиентов за период с {From} по {To}", from, to);

        var result = orderRepository.GetAll()
            .Where(o => o.AcceptDate >= from && o.AcceptDate <= to)
            .GroupBy(o => o.Client)
            .Select(group => new TopClientDto(
                group.Key.Id,
                group.Key.FullName,
                group.Count()
            ))
            .OrderByDescending(x => x.ItemsCount)
            .Take(5)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Запрос 3: Клиенты с наиболее долгими заказами, упорядоченные по ФИО
    /// </summary>
    [HttpGet("clients-with-longest-orders")]
    public ActionResult<IEnumerable<ClientDto>> GetClientsWithLongestOrders()
    {
        logger.LogInformation("Запрос аналитики: клиенты с самыми долгими заказами");

        var orders = orderRepository.GetAll().ToList();
        if (orders.Count == 0)
        {
            logger.LogWarning("Заказы отсутствуют, возвращен пустой список");
            return Ok(Enumerable.Empty<ClientDto>());
        }

        var maxDays = orders.Max(o => o.ExeDays);

        var result = orders
            .Where(o => o.ExeDays == maxDays)
            .Select(o => o.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .Select(c => new ClientDto(c.Id, c.FullName, c.PhoneNumber))
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Запрос 4.1: Топ-5 наиболее популярных категорий изделий за последний год
    /// </summary>
    [HttpGet("categories-most-popular")]
    public ActionResult<IEnumerable<CategoryPopularityDto>> GetMostPopularCategories()
    {
        logger.LogInformation("Запрос аналитики: самые популярные категории за год");
        var oneYearAgo = DataSeed.BaseDate.AddDays(-365);

        var result = orderRepository.GetAll()
            .Where(o => o.AcceptDate >= oneYearAgo)
            .GroupBy(o => o.Item.Category)
            .Select(group => new CategoryPopularityDto(group.Key.Name, group.Count()))
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Запрос 4.2: Топ-5 наименее популярных категорий изделий за последний год
    /// </summary>
    [HttpGet("categories-least-popular")]
    public ActionResult<IEnumerable<CategoryPopularityDto>> GetLeastPopularCategories()
    {
        logger.LogInformation("Запрос аналитики: наименее популярные категории за год");
        var oneYearAgo = DataSeed.BaseDate.AddDays(-365);

        var result = orderRepository.GetAll()
            .Where(o => o.AcceptDate >= oneYearAgo)
            .GroupBy(o => o.Item.Category)
            .Select(group => new CategoryPopularityDto(group.Key.Name, group.Count()))
            .OrderBy(x => x.OrdersCount)
            .Take(5)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Запрос 5: Клиенты, потратившие наибольшую сумму за весь период (с поддержкой нескольких лидеров)
    /// </summary>
    [HttpGet("top-spending-client")]
    public ActionResult<IEnumerable<TopSpendingClientDto>> GetTopSpendingClient()
    {
        logger.LogInformation("Запрос аналитики: клиент с наибольшими тратами");
        var clientSpending = orderRepository.GetAll()
            .GroupBy(o => o.Client)
            .Select(group => new TopSpendingClientDto(
                group.Key.Id,
                group.Key.FullName,
                group.Sum(o => o.Item.Category.Price)
            ))
            .ToList();

        if (clientSpending.Count == 0)
        {
            logger.LogWarning("Заказы отсутствуют, невозможно вычислить топ клиента");
            return NotFound(new { message = "Заказы отсутствуют" });
        }

        var maxSpent = clientSpending.Max(x => x.TotalSpent);
        var result = clientSpending
            .Where(x => x.TotalSpent == maxSpent)
            .OrderBy(x => x.FullName)
            .ToList();

        return Ok(result);
    }
}
