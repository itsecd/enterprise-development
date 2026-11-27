using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// HTTP-запросы для работы с позициями заказов.
/// </summary>
public static class OrderItemEndpoints
{
    /// <summary>
    /// Регистрирует запросы для работы с коллекцией.
    /// </summary>
    public static void Map(WebApplication app, FoodDeliveryData data)
    {
        var group = app.MapGroup("/api/order-items");

        group.MapGet("/", () =>
        {
            lock (data.SyncRoot)
            {
                return Results.Ok(data.OrderItems.ToArray());
            }
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.OrderItems.Find(item => item.Id == id);
                return entity is null ? Results.NotFound() : Results.Ok(entity);
            }
        });

        group.MapPost("/", (OrderItem entity) =>
        {
            lock (data.SyncRoot)
            {
                if (!(data.Orders.Any(order => order.Id == entity.OrderId) && data.Dishes.Any(dish => dish.Id == entity.DishId)))
                    return Results.BadRequest("Связанная сущность не найдена");

                entity.Id = data.OrderItems.Count == 0 ? 1 : data.OrderItems.Max(item => item.Id) + 1;
                data.OrderItems.Add(entity);
                return Results.Created($"/api/order-items/{entity.Id}", entity);
            }
        });

        group.MapPut("/{id:int}", (int id, OrderItem entity) =>
        {
            lock (data.SyncRoot)
            {
                var index = data.OrderItems.FindIndex(item => item.Id == id);
                if (index == -1)
                    return Results.NotFound();
                if (!(data.Orders.Any(order => order.Id == entity.OrderId) && data.Dishes.Any(dish => dish.Id == entity.DishId)))
                    return Results.BadRequest("Связанная сущность не найдена");

                entity.Id = id;
                data.OrderItems[index] = entity;
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.OrderItems.Find(item => item.Id == id);
                if (entity is null)
                    return Results.NotFound();

                data.OrderItems.Remove(entity);
                return Results.NoContent();
            }
        });
    }
}
