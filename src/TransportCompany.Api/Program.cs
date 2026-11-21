using Microsoft.EntityFrameworkCore;
using TransportCompany.Application.Services;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.MsSqlServer.Data;
using TransportCompany.Infrastructure.MsSqlServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
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

builder.AddSqlServerDbContext<TransportCompanyDbContext>("TransportCompanyDb");

// Репозитории
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();

// Сервисы
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TransportCompanyDbContext>();
    await dbContext.Database.MigrateAsync();

    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "TransportCompany API v1");
    });
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    await next(context);

    if (context.Response.StatusCode == StatusCodes.Status415UnsupportedMediaType)
    {
        app.Logger.LogWarning("Ответ 415 на запрос {Method} {Path}. Content-Type: {ContentType}",
            context.Request.Method,
            context.Request.Path,
            context.Request.ContentType);
    }
});

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();