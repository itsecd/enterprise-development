using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Application.Services;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.EfCore;
using AutoServiceLab.Infrastructure.EfCore.Repositories;
using AutoServiceLab.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<AutoServiceDbContext>("Postgres", configureDbContextOptions: options =>
{
    options.UseNpgsql(npgsqlOptions =>
        npgsqlOptions.MigrationsAssembly("AutoServiceLab.Infrastructure.EfCore"));
});

builder.Services.AddScoped<IClientRepository, ClientEfCoreRepository>();
builder.Services.AddScoped<ICarRepository, CarEfCoreRepository>();
builder.Services.AddScoped<IMechanicRepository, MechanicEfCoreRepository>();
builder.Services.AddScoped<IWorkTypeRepository, WorkTypeEfCoreRepository>();
builder.Services.AddScoped<IServiceOrderRepository, ServiceOrderEfCoreRepository>();
builder.Services.AddScoped<IOrderWorkRepository, OrderWorkEfCoreRepository>();
builder.Services.AddScoped<IOrderMechanicRepository, OrderMechanicEfCoreRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IMechanicService, MechanicService>();
builder.Services.AddScoped<IWorkTypeService, WorkTypeService>();
builder.Services.AddScoped<IServiceOrderService, ServiceOrderService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<AutoServiceDbContext>();
    context.Database.Migrate();
}

app.MapDefaultEndpoints();

//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
