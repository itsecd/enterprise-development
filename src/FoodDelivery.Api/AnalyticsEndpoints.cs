using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// Аналитические запросы к данным службы доставки.
/// </summary>
public static class AnalyticsEndpoints
{
    /// <summary>
    /// Регистрирует аналитические HTTP-запросы.
    /// </summary>
    /// <param name="app">Приложение, в котором регистрируются маршруты.</param>
    /// <param name="data">Данные службы доставки.</param>
    public static void Map(WebApplication app, FoodDeliveryData data)
    {
        var clients = data.Clients;
        var restaurants = data.Restaurants;
        var categories = data.Categories;
        var dishes = data.Dishes;
        var orders = data.Orders;
        var items = data.OrderItems;

        app.MapGet("/api/analytics/top-restaurants", () =>
        {
            lock (data.SyncRoot) return Results.Ok(orders.GroupBy(o => o.RestaurantId)
                .Select(g => new { RestaurantId = g.Key, OrderCount = g.Count() })
                .OrderByDescending(x => x.OrderCount).ThenBy(x => x.RestaurantId).Take(5).ToArray());
        });
        app.MapGet("/api/analytics/fastest-orders", () =>
        {
            lock (data.SyncRoot)
            {
                if (orders.Count == 0) return Results.Ok(Array.Empty<Order>());
                var minimum = orders.Min(o => o.DeliveryTime);
                return Results.Ok(orders.Where(o => o.DeliveryTime == minimum).OrderBy(o => o.Id).ToArray());
            }
        });
        app.MapGet("/api/analytics/restaurants/{id:int}/clients", (int id) =>
        {
            lock (data.SyncRoot)
            {
                if (!restaurants.Any(r => r.Id == id)) return Results.NotFound();
                var clientIds = orders.Where(o => o.RestaurantId == id).Select(o => o.ClientId).ToHashSet();
                return Results.Ok(clients.Where(c => clientIds.Contains(c.Id)).OrderBy(c => c.FullName).ToArray());
            }
        });
        app.MapGet("/api/analytics/category-statistics", (DateTimeOffset start, DateTimeOffset end) =>
        {
            lock (data.SyncRoot)
            {
                if (start >= end) return Results.BadRequest("Начало периода должно быть раньше конца");
                var result = from item in items
                             join order in orders on item.OrderId equals order.Id
                             join dish in dishes on item.DishId equals dish.Id
                             join category in categories on dish.CategoryId equals category.Id
                             where order.CreatedAt >= start && order.CreatedAt < end
                             group order by new { CategoryId = category.Id, CategoryName = category.Name } into g
                             let categoryOrders = g.DistinctBy(o => o.Id).ToArray()
                             orderby g.Key.CategoryId
                             select new { g.Key.CategoryId, g.Key.CategoryName, OrderCount = categoryOrders.Length,
                                 AverageOrderAmount = categoryOrders.Average(o => o.TotalAmount),
                                 TotalOrderAmount = categoryOrders.Sum(o => o.TotalAmount) };
                return Results.Ok(result.ToArray());
            }
        });
        app.MapGet("/api/analytics/top-clients", () =>
        {
            lock (data.SyncRoot)
            {
                var spending = orders.GroupBy(o => o.ClientId)
                    .Select(g => new { ClientId = g.Key, TotalSpent = g.Sum(o => o.TotalAmount) }).ToArray();
                if (spending.Length == 0) return Results.Ok(spending);
                var maximum = spending.Max(x => x.TotalSpent);
                return Results.Ok(spending.Where(x => x.TotalSpent == maximum).OrderBy(x => x.ClientId).ToArray());
            }
        });

    }
}
