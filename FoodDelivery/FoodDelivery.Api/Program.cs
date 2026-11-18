using FoodDelivery.Api.ErrorHandling;
using FoodDelivery.Application.Contracts.Analytics;
using FoodDelivery.Application.Contracts.Categories;
using FoodDelivery.Application.Contracts.Common;
using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Contracts.Restaurants;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Repositories;
using FoodDelivery.Infrastructure.InMemory.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

builder.Services.AddSingleton<FoodDeliveryData>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddSingleton<IDishRepository, DishRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IAnalyticsRepository, AnalyticsRepository>();

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
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API службы доставки еды");
    });
}

app.MapControllers();

app.Run();
