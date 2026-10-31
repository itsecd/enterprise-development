using CarWash.Application.Contracts;
using CarWash.Application.Contracts.Cars;
using CarWash.Application.Contracts.Customers;
using CarWash.Application.Contracts.Orders;
using CarWash.Application.Contracts.Procedures;
using CarWash.Application.Services;
using CarWash.Domain;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using CarWash.Infrastructure.InMemory.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Единый источник данных для всех репозиториев и аналитики
builder.Services.AddSingleton<CarWashData>();

// Репозитории
builder.Services.AddSingleton<IRepository<CarWashCustomer, int>, CarWashCustomerInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Car, int>, CarInMemoryRepository>();
builder.Services.AddSingleton<IRepository<CarWashProcedure, int>, CarWashProcedureInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Order, int>, OrderInMemoryRepository>();

// CRUD-службы
builder.Services.AddScoped<ICarWashCustomerService, CarWashCustomerService>();
builder.Services.AddScoped<IApplicationService<CarDto, CarCreateUpdateDto, int>,
    CarService>();
builder.Services.AddScoped<IApplicationService<CarWashProcedureDto, CarWashProcedureCreateUpdateDto, int>,
    CarWashProcedureService>();
builder.Services.AddScoped<IApplicationService<OrderDto, OrderCreateUpdateDto, int>,
    OrderService>();

// Аналитическая служба
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// Контроллеры и Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var assembly = Assembly.GetExecutingAssembly();
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"));

    foreach (var refAssembly in assembly.GetReferencedAssemblies())
    {
        if (refAssembly.Name!.StartsWith("System.") ||
            refAssembly.Name.StartsWith("Microsoft."))
        {
            continue;
        }

        var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{refAssembly.Name}.xml");

        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
