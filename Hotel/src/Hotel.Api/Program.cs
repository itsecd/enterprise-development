using Hotel.Api.Middleware;
using Hotel.Application.Services;
using Hotel.Contracts.Interfaces;
using Hotel.Domain.Data;
using Hotel.Domain.Interfaces;
using Hotel.Infrastructure.InMemory;
using Hotel.Infrastructure.InMemory.Repositories;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Логируем поля, которые не прошли автоматическую проверку входной модели.
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .ConfigureApiBehaviorOptions(options =>
    {
        var defaultResponseFactory = options.InvalidModelStateResponseFactory;

        options.InvalidModelStateResponseFactory = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            var invalidFields = context.ModelState
                .Where(entry => entry.Value?.Errors.Count > 0)
                .Select(entry => entry.Key);

            logger.LogWarning("Некорректные данные запроса {Method} {Path}. Поля: {Fields}",
                context.HttpContext.Request.Method,
                context.HttpContext.Request.Path,
                string.Join(", ", invalidFields));

            return defaultResponseFactory(context);
        };
    });

builder.Services.AddOpenApi();

// Один набор тестовых данных используется всеми запросами до остановки приложения.
builder.Services.AddSingleton<TestData>();
builder.Services.AddSingleton<IRelationshipLock, RelationshipLock>();

// Репозитории работают с общими коллекциями из TestData.
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

// Сервисы содержат бизнес-правила и преобразуют доменные модели в DTO.
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Hotel API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

/// <summary>Точка входа API, доступная для интеграционных тестов.</summary>
public partial class Program
{
}
