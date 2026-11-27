using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// HTTP-запросы для работы с категориями.
/// </summary>
public static class CategoryEndpoints
{
    /// <summary>
    /// Регистрирует запросы для работы с коллекцией.
    /// </summary>
    public static void Map(WebApplication app, FoodDeliveryData data)
    {
        var group = app.MapGroup("/api/categories");

        group.MapGet("/", () =>
        {
            lock (data.SyncRoot)
            {
                return Results.Ok(data.Categories.ToArray());
            }
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Categories.Find(item => item.Id == id);
                return entity is null ? Results.NotFound() : Results.Ok(entity);
            }
        });

        group.MapPost("/", (DishCategory entity) =>
        {
            lock (data.SyncRoot)
            {

                entity.Id = data.Categories.Count == 0 ? 1 : data.Categories.Max(item => item.Id) + 1;
                data.Categories.Add(entity);
                return Results.Created($"/api/categories/{entity.Id}", entity);
            }
        });

        group.MapPut("/{id:int}", (int id, DishCategory entity) =>
        {
            lock (data.SyncRoot)
            {
                var index = data.Categories.FindIndex(item => item.Id == id);
                if (index == -1)
                    return Results.NotFound();

                entity.Id = id;
                data.Categories[index] = entity;
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Categories.Find(item => item.Id == id);
                if (entity is null)
                    return Results.NotFound();
                if (data.Dishes.Any(dish => dish.CategoryId == id))
                    return Results.Conflict("На сущность есть ссылки");

                data.Categories.Remove(entity);
                return Results.NoContent();
            }
        });
    }
}
