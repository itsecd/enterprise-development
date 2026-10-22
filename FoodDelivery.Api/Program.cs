using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Application.Services;
using FoodDelivery.Infrastructure.InMemory.Repositories;
using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
