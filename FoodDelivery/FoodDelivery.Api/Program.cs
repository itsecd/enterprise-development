using FoodDelivery.Api.ErrorHandling;
using FoodDelivery.Application.Contracts.Analytics;
using FoodDelivery.Application.Contracts.Categories;
using FoodDelivery.Application.Contracts.Common;
using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Contracts.Restaurants;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Repositories;
using FoodDelivery.Infrastructure.EfCore;
using FoodDelivery.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var connectionString = builder.Configuration.GetConnectionString("fooddelivery")
    ?? throw new InvalidOperationException("Не задана строка подключения к MySQL.");

builder.Services.AddDbContext<FoodDeliveryDbContext>(options => options.UseMySQL(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

builder.Services.AddScoped<IOrderWriteTransaction, OrderWriteTransaction>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();

builder.Services.AddScoped<ICrudService<CategoryDto, CategoryRequest>, CategoryService>();
builder.Services.AddScoped<ICrudService<CustomerDto, CustomerRequest>, CustomerService>();
builder.Services.AddScoped<ICrudService<RestaurantDto, RestaurantRequest>, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var database = scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>();
    await database.Database.MigrateAsync();

    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API службы доставки еды");
    });
}

app.MapControllers();
app.MapDefaultEndpoints();

app.Run();
