using Carwash.Application.Services;
using Carwash.Application.Contracts;
using Carwash.Domain;
using Carwash.Domain.Entities;
using Carwash.Infrastructure.EfCore;
using Carwash.Infrastructure.EfCore.Repositories;
using Carwash.Application.Contracts.Clients;
using Carwash.Application.Contracts.Cars;
using Carwash.Application.Contracts.Services;
using Carwash.Application.Contracts.Orders;
using Scalar.AspNetCore;
using Carwash.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<CarwashDbContext>("SqlServer");

builder.Services.AddTransient<IRepository<Client, int>, ClientEfCoreRepository>();
builder.Services.AddTransient<IRepository<Car, int>, CarEfCoreRepository>();
builder.Services.AddTransient<IRepository<Service, int>, ServiceEfCoreRepository>();
builder.Services.AddTransient<IRepository<Order, int>, OrderEfCoreRepository>();

builder.Services.AddScoped<IApplicationService<ClientDto, ClientCreateUpdateDto, int>, ClientService>();
builder.Services.AddScoped<IApplicationService<CarDto, CarCreateUpdateDto, int>, CarService>();
builder.Services.AddScoped<IApplicationService<ServiceDto, ServiceCreateUpdateDto, int>, ServiceService>();
builder.Services.AddScoped<IApplicationService<OrderDto, OrderCreateUpdateDto, int>, OrderService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<CarwashDbContext>();
    context.Database.Migrate();
}

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
