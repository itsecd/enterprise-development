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

app.Run();
