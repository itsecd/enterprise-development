using DryCleaning.Api.Middleware;
using DryCleaning.Application.Services;
using DryCleaning.Contracts.Interfaces;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Domain.Seeds;
using DryCleaning.Infrastructure.InMemory.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TestData>();

builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<IItemCategoryRepository, ItemCategoryRepository>();
builder.Services.AddSingleton<IItemRepository, ItemRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IItemCategoryService, ItemCategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
