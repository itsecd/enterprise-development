using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Repositories;
using FoodDelivery.Infrastructure.Data;
using FoodDelivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<FoodDeliveryDbContext>("fooddeliverydb");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Обобщённые репозитории поверх EF Core
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// Сервисы бизнес-логики
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDishService, DishService>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>();
    await dbContext.Database.MigrateAsync();

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