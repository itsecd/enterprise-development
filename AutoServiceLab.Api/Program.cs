using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Application.Services;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<ICarRepository, CarRepository>();
builder.Services.AddSingleton<IMechanicRepository, MechanicRepository>();
builder.Services.AddSingleton<IWorkTypeRepository, WorkTypeRepository>();
builder.Services.AddSingleton<IServiceOrderRepository, ServiceOrderRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IMechanicService, MechanicService>();
builder.Services.AddScoped<IWorkTypeService, WorkTypeService>();
builder.Services.AddScoped<IServiceOrderService, ServiceOrderService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddSingleton<IOrderWorkRepository, OrderWorkRepository>();
builder.Services.AddSingleton<IOrderMechanicRepository, OrderMechanicRepository>();

builder.Services.AddControllers();

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
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
