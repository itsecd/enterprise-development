using System.Text.Json.Serialization;
using FitnessClub.Api.Infrastructure;
using FitnessClub.Application;
using FitnessClub.Infrastructure.PostgreSql;
using FitnessClub.Infrastructure.PostgreSql.Data;
using FitnessClub.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<FitnessClubDbContext>("fitnessclub");
builder.Services.AddPostgreSqlRepositories();

// CRUD- и аналитические сервисы
builder.Services.AddApplicationServices();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Swagger с XML-комментариями всех сборок решения
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FitnessClub API",
        Version = "v1",
        Description = "REST API фитнес-клуба: CRUD-операции и аналитика",
    });

    foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
    {
        options.IncludeXmlComments(xmlFile);
    }
});

// Глобальный обработчик ошибок
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Заполнение БД тестовыми данными
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FitnessClubDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.MapControllers();
app.MapDefaultEndpoints();
app.Run();
