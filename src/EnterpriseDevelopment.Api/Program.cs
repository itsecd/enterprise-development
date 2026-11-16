using EnterpriseDevelopment.Infrastructure.Persistence;
using Scalar.AspNetCore;
using EnterpriseDevelopment.Api.Exceptions;
using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI
builder.Services.AddOpenApi();

// Контроллеры
builder.Services.AddControllers();

// Обработка исключений
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Подключение PostgreSQL через Aspire
builder.AddNpgsqlDbContext<ApplicationDbContext>("fooddeliverydb");

// Репозитории PostgreSQL
builder.Services.AddScoped<IRestaurantRepository, EfRestaurantRepository>();
builder.Services.AddScoped<IClientRepository, EfClientRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<IDishRepository, EfDishRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();

// CRUD-сервисы
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Сервис аналитики
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
