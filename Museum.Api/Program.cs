using System.Text.Json.Serialization;
using Museum.Application.DependencyInjection;
using Museum.Infrastructure.InMemory.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Подключение сервисов приложения и хранилища
builder.Services
    .AddApplication()
    .AddInMemoryPersistence();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();
