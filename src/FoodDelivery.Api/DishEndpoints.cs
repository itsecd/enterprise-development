using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// HTTP-запросы для работы с блюдами.
/// </summary>
public static class DishEndpoints
{
    /// <summary>
    /// Регистрирует запросы для работы с коллекцией.
    /// </summary>
    /// <param name="app">Приложение, в котором регистрируются маршруты.</param>
    /// <param name="data">Данные службы доставки.</param>
    public static void Map(WebApplication app, FoodDeliveryData data)
    {
        var group = app.MapGroup("/api/dishes");

        group.MapGet("/", () =>
        {
            lock (data.SyncRoot)
            {
                return Results.Ok(data.Dishes.ToArray());
            }
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Dishes.Find(item => item.Id == id);
                return entity is null ? Results.NotFound() : Results.Ok(entity);
            }
        });

        group.MapPost("/", (Dish entity) =>
        {
            lock (data.SyncRoot)
            {
                if (!(data.Categories.Any(category => category.Id == entity.CategoryId) && data.Restaurants.Any(restaurant => restaurant.Id == entity.RestaurantId)))
                    return Results.BadRequest("Связанная сущность не найдена");

                entity.Id = data.Dishes.Count == 0 ? 1 : data.Dishes.Max(item => item.Id) + 1;
                data.Dishes.Add(entity);
                return Results.Created($"/api/dishes/{entity.Id}", entity);
            }
        });

        group.MapPut("/{id:int}", (int id, Dish entity) =>
        {
            lock (data.SyncRoot)
            {
                var index = data.Dishes.FindIndex(item => item.Id == id);
                if (index == -1)
                    return Results.NotFound();
                if (!(data.Categories.Any(category => category.Id == entity.CategoryId) && data.Restaurants.Any(restaurant => restaurant.Id == entity.RestaurantId)))
                    return Results.BadRequest("Связанная сущность не найдена");

                entity.Id = id;
                data.Dishes[index] = entity;
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Dishes.Find(item => item.Id == id);
                if (entity is null)
                    return Results.NotFound();
                if (data.OrderItems.Any(item => item.DishId == id))
                    return Results.Conflict("На сущность есть ссылки");

                data.Dishes.Remove(entity);
                return Results.NoContent();
            }
        });
    }
}
