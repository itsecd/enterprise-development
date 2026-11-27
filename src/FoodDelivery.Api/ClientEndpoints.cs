using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// HTTP-запросы для работы с клиентами.
/// </summary>
public static class ClientEndpoints
{
    /// <summary>
    /// Регистрирует запросы для работы с коллекцией.
    /// </summary>
    public static void Map(WebApplication app, FoodDeliveryData data)
    {
        var group = app.MapGroup("/api/clients");

        group.MapGet("/", () =>
        {
            lock (data.SyncRoot)
            {
                return Results.Ok(data.Clients.ToArray());
            }
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Clients.Find(item => item.Id == id);
                return entity is null ? Results.NotFound() : Results.Ok(entity);
            }
        });

        group.MapPost("/", (Client entity) =>
        {
            lock (data.SyncRoot)
            {

                entity.Id = data.Clients.Count == 0 ? 1 : data.Clients.Max(item => item.Id) + 1;
                data.Clients.Add(entity);
                return Results.Created($"/api/clients/{entity.Id}", entity);
            }
        });

        group.MapPut("/{id:int}", (int id, Client entity) =>
        {
            lock (data.SyncRoot)
            {
                var index = data.Clients.FindIndex(item => item.Id == id);
                if (index == -1)
                    return Results.NotFound();

                entity.Id = id;
                data.Clients[index] = entity;
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            lock (data.SyncRoot)
            {
                var entity = data.Clients.Find(item => item.Id == id);
                if (entity is null)
                    return Results.NotFound();
                if (data.Orders.Any(order => order.ClientId == id))
                    return Results.Conflict("На сущность есть ссылки");

                data.Clients.Remove(entity);
                return Results.NoContent();
            }
        });
    }
}
