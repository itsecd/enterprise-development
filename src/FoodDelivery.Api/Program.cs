using FoodDelivery.Domain;
using FoodDelivery.Domain.Entities;
using System.Text.Json.Serialization.Metadata;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Relationships are represented by IDs in the API.
    options.SerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers = { type =>
        {
            if (type.Kind != JsonTypeInfoKind.Object) return;
            foreach (var property in type.Properties.Where(p => p.PropertyType == typeof(Client)
                || p.PropertyType == typeof(Restaurant) || p.PropertyType == typeof(DishCategory)
                || p.PropertyType == typeof(Dish) || p.PropertyType == typeof(Order)
                || p.PropertyType == typeof(List<OrderItem>)).ToArray())
                type.Properties.Remove(property);
        }}
    };
});
var app = builder.Build();
var seed = new DataSeeder();
var clients = seed.Clients.ToList();
var restaurants = seed.Restaurants.ToList();
var categories = seed.DishCategories.ToList();
var dishes = seed.Dishes.ToList();
var orders = seed.Orders.ToList();
var items = seed.OrderItems.ToList();
var gate = new object();

MapCrud("clients", clients, x => x.Id, (x, id) => x.Id = id,
    _ => true, id => orders.Any(x => x.ClientId == id));
MapCrud("restaurants", restaurants, x => x.Id, (x, id) => x.Id = id,
    _ => true, id => orders.Any(x => x.RestaurantId == id) || dishes.Any(x => x.RestaurantId == id));
MapCrud("categories", categories, x => x.Id, (x, id) => x.Id = id,
    _ => true, id => dishes.Any(x => x.CategoryId == id));
MapCrud("dishes", dishes, x => x.Id, (x, id) => x.Id = id,
    x => categories.Any(c => c.Id == x.CategoryId) && restaurants.Any(r => r.Id == x.RestaurantId),
    id => items.Any(x => x.DishId == id));
MapCrud("orders", orders, x => x.Id, (x, id) => x.Id = id,
    x => clients.Any(c => c.Id == x.ClientId) && restaurants.Any(r => r.Id == x.RestaurantId),
    id => items.Any(x => x.OrderId == id));
MapCrud("order-items", items, x => x.Id, (x, id) => x.Id = id,
    x => orders.Any(o => o.Id == x.OrderId) && dishes.Any(d => d.Id == x.DishId), _ => false);

app.MapGet("/api/analytics/top-restaurants", () =>
{
    lock (gate) return Results.Ok(orders.GroupBy(o => o.RestaurantId)
        .Select(g => new { RestaurantId = g.Key, OrderCount = g.Count() })
        .OrderByDescending(x => x.OrderCount).ThenBy(x => x.RestaurantId).Take(5).ToArray());
});
app.MapGet("/api/analytics/fastest-orders", () =>
{
    lock (gate)
    {
        if (orders.Count == 0) return Results.Ok(Array.Empty<Order>());
        var minimum = orders.Min(o => o.DeliveryTime);
        return Results.Ok(orders.Where(o => o.DeliveryTime == minimum).OrderBy(o => o.Id).ToArray());
    }
});
app.MapGet("/api/analytics/restaurants/{id:int}/clients", (int id) =>
{
    lock (gate)
    {
        if (!restaurants.Any(r => r.Id == id)) return Results.NotFound();
        var clientIds = orders.Where(o => o.RestaurantId == id).Select(o => o.ClientId).ToHashSet();
        return Results.Ok(clients.Where(c => clientIds.Contains(c.Id)).OrderBy(c => c.FullName).ToArray());
    }
});
app.Run();

void MapCrud<T>(string path, List<T> data, Func<T, int> getId, Action<T, int> setId,
    Func<T, bool> validReferences, Func<int, bool> isReferenced) where T : class
{
    var group = app.MapGroup($"/api/{path}");
    group.MapGet("/", () => { lock (gate) return Results.Ok(data.ToArray()); });
    group.MapGet("/{id:int}", (int id) =>
    {
        lock (gate) { var entity = data.Find(x => getId(x) == id); return entity is null ? Results.NotFound() : Results.Ok(entity); }
    });
    group.MapPost("/", (T entity) =>
    {
        lock (gate)
        {
            if (!validReferences(entity)) return Results.BadRequest("Referenced entity does not exist");
            var id = data.Count == 0 ? 1 : data.Max(getId) + 1;
            setId(entity, id);
            data.Add(entity);
            return Results.Created($"/api/{path}/{id}", entity);
        }
    });
    group.MapPut("/{id:int}", (int id, T entity) =>
    {
        lock (gate)
        {
            var index = data.FindIndex(x => getId(x) == id);
            if (index < 0) return Results.NotFound();
            if (!validReferences(entity)) return Results.BadRequest("Referenced entity does not exist");
            setId(entity, id);
            data[index] = entity;
            return Results.NoContent();
        }
    });
    group.MapDelete("/{id:int}", (int id) =>
    {
        lock (gate)
        {
            var index = data.FindIndex(x => getId(x) == id);
            if (index < 0) return Results.NotFound();
            if (isReferenced(id)) return Results.Conflict("Entity is referenced by another entity");
            data.RemoveAt(index);
            return Results.NoContent();
        }
    });
}
