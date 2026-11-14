using System.Text.Json.Serialization;
using CarWash.Application.Services;
using CarWash.Contracts.Interfaces;
using CarWash.Infrastructure.EfCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("carwashdb")
    ?? throw new InvalidOperationException(
        "Не задана строка подключения 'carwashdb'. Запустите проект через CarWash.AppHost");

builder.Services.AddEfCoreInfrastructure(connectionString);

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

await app.Services.ApplyMigrationsAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();
