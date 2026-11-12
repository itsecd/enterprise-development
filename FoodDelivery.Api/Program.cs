using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;
using FoodDelivery.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Контекст с данными
builder.Services.AddSingleton(DataSeeder.Seed());

// Обобщённые репозитории
builder.Services.AddSingleton<IRepository<Client>>(sp =>
    new InMemoryRepository<Client>(sp.GetRequiredService<FoodDeliveryContext>().Clients));
builder.Services.AddSingleton<IRepository<Restaurant>>(sp =>
    new InMemoryRepository<Restaurant>(sp.GetRequiredService<FoodDeliveryContext>().Restaurants));
builder.Services.AddSingleton<IRepository<DishCategory>>(sp =>
    new InMemoryRepository<DishCategory>(sp.GetRequiredService<FoodDeliveryContext>().Categories));
builder.Services.AddSingleton<IRepository<Dish>>(sp =>
    new InMemoryRepository<Dish>(sp.GetRequiredService<FoodDeliveryContext>().Dishes));
builder.Services.AddSingleton<IRepository<Order>>(sp =>
    new InMemoryRepository<Order>(sp.GetRequiredService<FoodDeliveryContext>().Orders));

// Сервисы бизнес-логики
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDishService, DishService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Food Delivery API Reference")
               .WithTheme(ScalarTheme.Moon);
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();