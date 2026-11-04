using BeautySalon.Server.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Игнорируем циклические ссылки при сериализации
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Добавляем Swagger для документации API
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ISalonDataStore, InMemorySalonDataStore>();

var app = builder.Build();

// Настраиваем Swagger UI
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});;

app.MapControllers();

app.Run();