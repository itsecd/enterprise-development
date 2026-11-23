using BeautySalon.Domain.Repositories;
using BeautySalon.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using BeautySalon.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Добавляем поддержку контроллеров
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Игнорируем циклические ссылки при сериализации
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Регистрация DbContext через Aspire
builder.AddSqlServerDbContext<BeautySalonDbContext>("beauty-salon-db");

// Регистрация EF Core репозиториев
builder.Services.AddScoped<IClientRepository, EfClientRepository>();
builder.Services.AddScoped<IMasterRepository, EfMasterRepository>();
builder.Services.AddScoped<IServiceRepository, EfServiceRepository>();
builder.Services.AddScoped<IAppointmentRepository, EfAppointmentRepository>();

// Добавляем Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// Настраиваем Swagger UI
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});;

app.MapControllers();
app.MapDefaultEndpoints();

app.Run();