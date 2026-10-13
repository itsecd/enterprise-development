using Microsoft.AspNetCore.Mvc;
using DryCleaning.Api.Services;
using DryCleaning.Domain.Models;

namespace DryCleaning.Api.Controllers;

/// <summary>
/// Контроллер аналитических выборок
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly InMemStore _store;

    public AnalyticsController(InMemStore store)
    {
        _store = store;
    }

    /// <summary>
    /// Запрос 1: Заказы в обработке, упорядоченные по дате приема
    /// </summary>
    [HttpGet("orders-in-progress")]
    public ActionResult<IEnumerable<Order>> GetOrdersInProgress()
    {
        var result = _store.Orders
            .Where(o => o.Status == Status.InProgress)
            .OrderBy(o => o.AcceptDate)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Запрос 2: Топ-5 клиентов, сдавших больше всего изделий за заданный период
    /// </summary>
    [HttpGet("top-clients")]
    public IActionResult GetTopClients([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var from = startDate ?? InMemStore.BaseDate.AddDays(-30);
        var to = endDate ?? InMemStore.BaseDate;

        var result = _store.Orders
            .Where(o => o.AcceptDate >= from && o.AcceptDate <= to)
            .GroupBy(o => o.Client)
            .Select(group => new
            {
                Client = group.Key,
                ItemsCount = group.Count()
            })
            .OrderByDescending(x => x.ItemsCount)
            .Take(5)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Запрос 3: Клиенты с наиболее долгими заказами, упорядоченные по ФИО
    /// </summary>
    [HttpGet("clients-with-longest-orders")]
    public ActionResult<IEnumerable<Client>> GetClientsWithLongestOrders()
    {
        if (!_store.Orders.Any())
        {
            return Ok(Enumerable.Empty<Client>());
        }

        var maxDays = _store.Orders.Max(o => o.ExeDays);

        var result = _store.Orders
            .Where(o => o.ExeDays == maxDays)
            .Select(o => o.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Запрос 4: Топ-5 наиболее и наименее популярных категорий изделий за последний год
    /// </summary>
    [HttpGet("categories-popularity")]
    public IActionResult GetCategoriesPopularity()
    {
        var oneYearAgo = InMemStore.BaseDate.AddDays(-365);

        var categoryPopularity = _store.Orders
            .Where(o => o.AcceptDate >= oneYearAgo)
            .GroupBy(o => o.Item.Category)
            .Select(group => new
            {
                Category = group.Key,
                OrdersCount = group.Count()
            })
            .ToList();

        var mostPopular = categoryPopularity
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToList();

        var leastPopular = categoryPopularity
            .OrderBy(x => x.OrdersCount)
            .Take(5)
            .ToList();

        return Ok(new
        {
            MostPopular = mostPopular,
            LeastPopular = leastPopular
        });
    }

    /// <summary>
    /// Запрос 5: Клиенты, потратившие наибольшую сумму за весь период (с поддержкой нескольких лидеров)
    /// </summary>
    [HttpGet("top-spending-client")]
    public IActionResult GetTopSpendingClient()
    {
        var clientSpending = _store.Orders
            .GroupBy(o => o.Client)
            .Select(group => new
            {
                Client = group.Key,
                TotalSpent = group.Sum(o => o.Item.Category.Price)
            })
            .ToList();

        if (!clientSpending.Any())
        {
            return NotFound(new { message = "Заказы отсутствуют" });
        }

        var maxSpent = clientSpending.Max(x => x.TotalSpent);
        var result = clientSpending
            .Where(x => x.TotalSpent == maxSpent)
            .OrderBy(x => x.Client.FullName)
            .ToList();

        return Ok(result);
    }
}
