using CarWash.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(InMemoryDataStore dataStore) : ControllerBase
{
    /// <summary>
    /// 1. Вывести топ 5 клиентов по количеству посещений.
    /// </summary>
    [HttpGet("top-clients")]
    public IActionResult GetTop5Clients()
    {
        var result = dataStore.Orders
            .GroupBy(o => o.Client)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Id)
            .Take(5)
            .Select(g => new
            {
                g.Key.Id,
                g.Key.FullName,
                VisitCount = g.Count()
            })
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// 2. Вывести информацию об автомобилях, находящихся на мойке в данный момент.
    /// </summary>
    [HttpGet("current-cars")]
    public IActionResult GetCurrentCars([FromQuery] DateTime? time)
    {
        // Если время не передано в запросе, берем текущее время
        var currentTime = time ?? DateTime.Now;

        var result = dataStore.Orders
            .Where(o => o.StartTime <= currentTime && currentTime < o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .Select(o => new
            {
                o.Car.LicensePlate,
                o.Car.Brand,
                o.Service.Name,
                o.BoxNumber
            })
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// 3. Вывести топ 5 наиболее популярных услуг.
    /// </summary>
    [HttpGet("top-services")]
    public IActionResult GetTop5PopularServices()
    {
        var result = dataStore.Orders
            .GroupBy(o => o.Service)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Id)
            .Take(5)
            .Select(g => new
            {
                g.Key.Name,
                UsageCount = g.Count()
            })
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// 4. Узнать для выбранного бокса мойки, начиная с какого времени он освобождается.
    /// </summary>
    [HttpGet("box-free-time")]
    public IActionResult GetBoxFreeTime([FromQuery] int boxNumber, [FromQuery] DateTime? time)
    {
        var currentTime = time ?? DateTime.Now;

        var busyOrders = dataStore.Orders
            .Where(o => o.BoxNumber == boxNumber && o.StartTime.AddMinutes(o.Service.DurationMinutes) > currentTime)
            .OrderBy(o => o.StartTime)
            .ToList();

        var freeTime = currentTime;

        foreach (var order in busyOrders)
        {
            var orderEndTime = order.StartTime.AddMinutes(order.Service.DurationMinutes);

            if (order.StartTime <= freeTime)
            {
                if (orderEndTime > freeTime)
                {
                    freeTime = orderEndTime;
                }
            }
            else
            {
                break;
            }
        }

        return Ok(new { BoxNumber = boxNumber, FreeTime = freeTime });
    }

    /// <summary>
    /// 5. Для каждой услуги вывести суммарную выручку.
    /// </summary>
    [HttpGet("revenue")]
    public IActionResult GetRevenueForEachService()
    {
        var result = dataStore.Orders
            .GroupBy(o => o.Service.Name)
            .ToDictionary(g => g.Key, g => g.Sum(o => o.Service.Price));

        return Ok(result);
    }
}