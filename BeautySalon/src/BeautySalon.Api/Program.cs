using System.Text.Json.Serialization;
using BeautySalon.Api.ExceptionHandlers;
using BeautySalon.Application;
using BeautySalon.Infrastructure.InMemory;
using BeautySalon.Infrastructure.PostgreSQL;
using BeautySalon.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var provider = builder.Configuration["Storage:Provider"] ?? "PostgreSQL";
switch (provider)
{
    case "InMemory":
        builder.Services.AddInMemoryStorage();
        break;
    case "PostgreSQL":
        builder.AddPostgreSqlStorage("salon");
        break;
    default:
        throw new InvalidOperationException($"Неизвестное хранилище: {provider}.");
}

builder.Services.AddApplication();

builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

if (provider == "PostgreSQL")
{
    await using var scope = app.Services.CreateAsyncScope();
    var database = scope.ServiceProvider.GetRequiredService<SalonDbContext>();
    await database.Database.MigrateAsync();
}

app.MapControllers();
app.MapDefaultEndpoints();

app.Run();

/// <summary>
/// Точка входа API салона красоты. Класс открыт для интеграционных тестов.
/// </summary>
public partial class Program
{
}
