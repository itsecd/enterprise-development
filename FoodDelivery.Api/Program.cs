using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;
using FoodDelivery.Infrastructure.EfCore;
using FoodDelivery.Infrastructure.EfCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<FoodDeliveryDbContext>("fooddeliverydb");
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IRepository<Category>, EfRepository<Category>>();
builder.Services.AddScoped<IRepository<Customer>, EfRepository<Customer>>();
builder.Services.AddScoped<IRepository<Dish>, EfRepository<Dish>>();
builder.Services.AddScoped<IRepository<Restaurant>, EfRepository<Restaurant>>();
builder.Services.AddScoped<IRepository<OrderItem>, EfRepository<OrderItem>>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>();
    db.Database.Migrate();

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
