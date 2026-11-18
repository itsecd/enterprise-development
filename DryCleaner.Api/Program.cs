using System.Text.Json.Serialization;
using System.Globalization;
using System.Reflection;
using DryCleaner.Application.Contracts.Interfaces;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Repositories;
using DryCleaner.Application.Services;
using DryCleaner.Infrastructure.EfCore.Data;
using DryCleaner.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

builder.AddNpgsqlDbContext<DryCleanerDbContext>("drycleanerdb");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    var contractsXmlFile = "DryCleaner.Application.Contracts.xml";
    var contractsXmlPath = Path.Combine(AppContext.BaseDirectory, contractsXmlFile);
    if (File.Exists(contractsXmlPath))
    {
        options.IncludeXmlComments(contractsXmlPath);
    }
});

builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Item>, ItemRepository>();
builder.Services.AddScoped<IRepository<ItemCategory>, ItemCategoryRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemCategoryService, ItemCategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DryCleanerDbContext>();
    try
    {
        await dbContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при применении миграций базы данных.");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapDefaultEndpoints();

app.Run();