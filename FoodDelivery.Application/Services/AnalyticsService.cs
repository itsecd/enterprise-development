using Microsoft.Extensions.Logging;
using FoodDelivery.Contracts.Analytics;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Сервис для выполнения аналитических запросов по заказам, ресторанам и клиентам.
/// </summary>
public class AnalyticsService(
    IRepository<Order> orderRepo,
    IRepository<Client> clientRepo,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    public IReadOnlyList<TopRestaurantDto> GetTop5Restaurants()
    {
        logger.LogInformation("Запрос топ-5 ресторанов по количеству заказов.");
        return
        [
            .. orderRepo.GetAll()
                .Where(o => o.Restaurant != null)
                .GroupBy(o => o.Restaurant!)
                .Select(g => new TopRestaurantDto(g.Key.Id, g.Key.Name, g.Count()))
                .OrderByDescending(x => x.OrderCount)
                .ThenBy(x => x.Name)
                .Take(5)
        ];
    }

    public IReadOnlyList<MinDeliveryOrderDto> GetOrdersWithMinDeliveryTime()
    {
        logger.LogInformation("Запрос заказов с минимальным временем доставки.");
        List<Order> delivered = [.. orderRepo.GetAll().Where(o => o.DeliveryDateTime.HasValue)];
        if (delivered.Count == 0) return [];

        var minDuration = delivered.Min(o => o.DeliveryDateTime!.Value - o.OrderDateTime);

        return
        [
            .. delivered
                .Where(o => o.DeliveryDateTime!.Value - o.OrderDateTime == minDuration)
                .Select(o => new MinDeliveryOrderDto(
                    o.Id,
                    (o.DeliveryDateTime!.Value - o.OrderDateTime).TotalMinutes,
                    o.OrderDateTime,
                    o.DeliveryDateTime!.Value))
        ];
    }

    public IReadOnlyList<ClientSummaryDto> GetClientsByRestaurant(int restaurantId)
    {
        logger.LogInformation("Запрос клиентов ресторана {RestaurantId}.", restaurantId);
        return
        [
            .. orderRepo.GetAll()
                .Where(o => o.RestaurantId == restaurantId && o.Client != null)
                .Select(o => o.Client!)
                .DistinctBy(c => c.Id)
                .OrderBy(c => c.FullName)
                .Select(c => new ClientSummaryDto(c.Id, c.FullName, c.Phone, c.DeliveryAddress))
        ];
    }

    public IReadOnlyList<CategorySummaryDto> GetCategorySummary(DateTime start, DateTime end)
    {
        logger.LogInformation("Запрос сводки по категориям за период с {Start} по {End}.", start, end);
        return
        [
            .. orderRepo.GetAll()
                .Where(o => o.OrderDateTime >= start && o.OrderDateTime < end)
                .SelectMany(o => o.Dishes.Select(d => new { Category = d.Category!, Order = o }))
                .Where(x => x.Category != null)
                .GroupBy(x => x.Category.Id)
                .Select(g => new CategorySummaryDto(
                    g.Key,
                    g.First().Category.Name,
                    g.Select(x => x.Order.Id).Distinct().Count(),
                    g.Select(x => x.Order).DistinctBy(o => o.Id).Average(o => o.TotalPrice),
                    g.Select(x => x.Order).DistinctBy(o => o.Id).Sum(o => o.TotalPrice)))
                .OrderBy(x => x.CategoryId)
        ];
    }

    public IReadOnlyList<TopSpenderDto> GetTopSpenders()
    {
        logger.LogInformation("Запрос клиентов с максимальными расходами.");
        var spendings = orderRepo.GetAll()
            .GroupBy(o => o.ClientId)
            .Select(g => new
            {
                Client = clientRepo.GetById(g.Key),
                Total = g.Sum(o => o.TotalPrice)
            })
            .Where(x => x.Client != null)
            .ToList();

        if (spendings.Count == 0) return [];
        var maxSpent = spendings.Max(x => x.Total);

        return
        [
            .. spendings
                .Where(x => x.Total == maxSpent)
                .Select(x => new TopSpenderDto(x.Client!.Id, x.Client.FullName, x.Total))
        ];
    }
}