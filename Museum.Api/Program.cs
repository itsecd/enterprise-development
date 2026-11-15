using System.Text.Json.Serialization;
using Museum.Application.DependencyInjection;
using Museum.Infrastructure.MySql.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Подключение сервисов приложения и хранилища (база данных MySQL)
builder.Services
    .AddApplication()
    .AddMySqlPersistence(builder.Configuration.GetConnectionString("museum"));

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

var app = builder.Build();

// Создание таблиц и начальное заполнение при запуске
await app.Services.ApplyMigrationsAsync();

if (app.Environment.IsDevelopment())
{
    // OpenAPI-спецификация: /openapi/v1.json
    app.MapOpenApi();

    // Интерактивная документация: /scalar
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();
