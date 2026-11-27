using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// HTTP-запросы для работы с ресторанами.
/// </summary>
public static class RestaurantEndpoints
{
    /// <summary>
    /// Регистрирует запросы для работы с коллекцией.
    /// </summary>
    /// <param name="app">Приложение, в котором регистрируются маршруты.</param>
    /// <param name="data">Данные службы доставки.</param>
    public static void Map(WebApplication app, FoodDeliveryData data)
    {
        var group = app.MapGroup("/api/restaurants");

        group.MapGet("/", () =>
        {
            lock (data.SyncRoot)
            {
                return Results.Ok(data.Restaurants.ToArray());
            }
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Restaurants.Find(item => item.Id == id);
                return entity is null ? Results.NotFound() : Results.Ok(entity);
            }
        });

        group.MapPost("/", (Restaurant entity) =>
        {
            lock (data.SyncRoot)
            {
                entity.Id = data.Restaurants.Count == 0 ? 1 : data.Restaurants.Max(item => item.Id) + 1;
                data.Restaurants.Add(entity);
                return Results.Created($"/api/restaurants/{entity.Id}", entity);
            }
        });

        group.MapPut("/{id:int}", (int id, Restaurant entity) =>
        {
            lock (data.SyncRoot)
            {
                var index = data.Restaurants.FindIndex(item => item.Id == id);
                if (index == -1)
                    return Results.NotFound();

                entity.Id = id;
                data.Restaurants[index] = entity;
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Restaurants.Find(item => item.Id == id);
                if (entity is null)
                    return Results.NotFound();
                if (data.Orders.Any(order => order.RestaurantId == id) || data.Dishes.Any(dish => dish.RestaurantId == id))
                    return Results.Conflict("На сущность есть ссылки");

                data.Restaurants.Remove(entity);
                return Results.NoContent();
            }
        });
    }
}
