using System.Text.Json.Serialization;
using Museum.Application.DependencyInjection;
using Museum.Infrastructure.InMemory.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Подключение сервисов приложения и хранилища
builder.Services
    .AddApplication()
    .AddInMemoryPersistence();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // OpenAPI-спецификация: /openapi/v1.json
    app.MapOpenApi();

    // Интерактивная документация: /scalar
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();
