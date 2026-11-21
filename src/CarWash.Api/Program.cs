using System.Text.Json.Serialization;
using CarWash.Application.Services;
using CarWash.Contracts.Interfaces;
using CarWash.Infrastructure.EfCore;
using Scalar.AspNetCore;

using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

builder.AddMySqlDataSource("carwashdb", settings =>
{
    if (!string.IsNullOrEmpty(settings.ConnectionString))
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder(settings.ConnectionString)
        {
            AllowUserVariables = true,
            UseAffectedRows = false
        };
        settings.ConnectionString = connectionStringBuilder.ConnectionString;
    }
});

builder.Services.AddEfCoreInfrastructure();

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

app.MapDefaultEndpoints();

app.Run();
