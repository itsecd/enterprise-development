using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using VetClinic.Application;
using VetClinic.InMemory;
// Инициализация билдера
var builder = WebApplication.CreateBuilder(args);

// Настройка логирования
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Регистрация компонентов системы
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddOpenApi();

// Автономное подключение инкапсулированных слоев через их собственные DI-модули
builder.Services.AddDataLayer();
builder.Services.AddApplicationLayer();

// Сборка приложения
var app = builder.Build();

// Логируем успешный старт инфраструктурного контура сервера
var logger = app.Services.GetRequiredService<Microsoft.Extensions.Logging.ILogger<Program>>();
logger.LogInformation("Бэкенд-сервер VetClinic успешно запущен. Сборка слоев завершена.");

// Контейнер обработки HTTP решений
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("VetClinic Interactive API (Scalar)")
               .WithTheme(ScalarTheme.DeepSpace)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.MapControllers();

app.Run();
