using Scalar.AspNetCore;
using EnterpriseDevelopment.Api.Exceptions;
using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Application.Services;
using EnterpriseDevelopment.Domain;
using EnterpriseDevelopment.Infrastructure.Repositories;
using EnterpriseDevelopment.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI
builder.Services.AddOpenApi();

// Контроллеры
builder.Services.AddControllers();

// Обработка исключений
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Начальные данные
builder.Services.AddSingleton<DataSeeder>();

// Общее хранилище
builder.Services.AddSingleton<InMemoryDataStore>();

// Репозитории
builder.Services.AddSingleton<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<IDishRepository, DishRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

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
