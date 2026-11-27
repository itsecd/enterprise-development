using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// HTTP-запросы для работы с заказами.
/// </summary>
public static class OrderEndpoints
{
    /// <summary>
    /// Регистрирует запросы для работы с коллекцией.
    /// </summary>
    /// <param name="app">Приложение, в котором регистрируются маршруты.</param>
    /// <param name="data">Данные службы доставки.</param>
    public static void Map(WebApplication app, FoodDeliveryData data)
    {
        var group = app.MapGroup("/api/orders");

        group.MapGet("/", () =>
        {
            lock (data.SyncRoot)
            {
                return Results.Ok(data.Orders.ToArray());
            }
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Orders.Find(item => item.Id == id);
                return entity is null ? Results.NotFound() : Results.Ok(entity);
            }
        });

        group.MapPost("/", (Order entity) =>
        {
            lock (data.SyncRoot)
            {
                if (!(data.Clients.Any(client => client.Id == entity.ClientId) && data.Restaurants.Any(restaurant => restaurant.Id == entity.RestaurantId)))
                    return Results.BadRequest("Связанная сущность не найдена");

                entity.Id = data.Orders.Count == 0 ? 1 : data.Orders.Max(item => item.Id) + 1;
                data.Orders.Add(entity);
                return Results.Created($"/api/orders/{entity.Id}", entity);
            }
        });

        group.MapPut("/{id:int}", (int id, Order entity) =>
        {
            lock (data.SyncRoot)
            {
                var index = data.Orders.FindIndex(item => item.Id == id);
                if (index == -1)
                    return Results.NotFound();
                if (!(data.Clients.Any(client => client.Id == entity.ClientId) && data.Restaurants.Any(restaurant => restaurant.Id == entity.RestaurantId)))
                    return Results.BadRequest("Связанная сущность не найдена");

                entity.Id = id;
                data.Orders[index] = entity;
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Orders.Find(item => item.Id == id);
                if (entity is null)
                    return Results.NotFound();
                if (data.OrderItems.Any(item => item.OrderId == id))
                    return Results.Conflict("На сущность есть ссылки");

                data.Orders.Remove(entity);
                return Results.NoContent();
            }
        });
    }
}
