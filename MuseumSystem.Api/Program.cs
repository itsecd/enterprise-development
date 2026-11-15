using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Infrastructure.InMemory.Repositories;
using MuseumSystem.Services.Services;
using Scalar.AspNetCore;
using MuseumSystem.Api.Data;
using Microsoft.EntityFrameworkCore;
using MuseumSystem.Infrastructure.EfCore;
using MuseumSystem.Api.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Временная строка подключения
//var connectionString = builder.Configuration.GetConnectionString("museumdb")
//    ?? "Host=localhost;Database=museumdb;Username=postgres;Password=postgres";

//builder.Services.AddDbContext<MuseumDbContext>(options =>
//    options.UseNpgsql(connectionString)
//           .UseSnakeCaseNamingConvention());

builder.AddNpgsqlDbContext<MuseumDbContext>("museumdb",
    configureDbContextOptions: options =>
    {
        options.UseSnakeCaseNamingConvention();
    });

builder.Services.AddControllers();

builder.Services.AddSingleton<HashTableUserStore>();

// Регистрация репозиториев в зависимости от конфигурации
var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryStorage");

if (useInMemory)
{
    // In-memory реализация
    builder.Services.AddSingleton<IRepository<Exhibition>>(sp =>
        new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions()));

    builder.Services.AddSingleton<IRepository<Visitor>>(sp =>
        new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors()));

    builder.Services.AddSingleton<IRepository<Excursion>>(sp =>
    {
        var exhibitionRepo = sp.GetRequiredService<IRepository<Exhibition>>();
        var exhibitions = exhibitionRepo.GetAllAsync().Result.ToList();
        return new InMemoryRepository<Excursion>(MuseumDataSeeder.GetExcursions(exhibitions));
    });

    builder.Services.AddSingleton<IRepository<Ticket>>(sp =>
    {
        var excursionRepo = sp.GetRequiredService<IRepository<Excursion>>();
        var visitorRepo = sp.GetRequiredService<IRepository<Visitor>>();
        var excursions = excursionRepo.GetAllAsync().Result.ToList();
        var visitors = visitorRepo.GetAllAsync().Result.ToList();
        return new InMemoryRepository<Ticket>(MuseumDataSeeder.GetTickets(excursions, visitors));
    });
}
else
{
    // EF Core реализация
    builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<MuseumDbContext>());

    builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
}

// Регистрация сервисов
builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
builder.Services.AddScoped<IExcursionService, ExcursionService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// Настройка нативного OpenApi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // Инициализация компонентов
        document.Components ??= new OpenApiComponents();

        // Создаем схему безопасности Bearer
        var bearerScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Введите JWT токен (без слова 'Bearer')"
        };
        document.AddComponent("Bearer", bearerScheme);

        // Создаем требование безопасности через OpenApiSecuritySchemeReference
        var securityRequirement = new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        };

        // Применяем требование ко всем операциям
        foreach (var path in document.Paths.Values)
        {
            // Проверка на null для устранения предупреждения CS8602
            if (path.Operations != null)
            {
                foreach (var operation in path.Operations.Values)
                {
                    operation.Security ??= new List<OpenApiSecurityRequirement>();
                    operation.Security.Add(securityRequirement);
                }
            }
        }

        return Task.CompletedTask;
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("SecretKey не найден в конфигурации");
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

var app = builder.Build();

// Автоматическое применение миграций при запуске
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<MuseumDbContext>();
        dbContext.Database.Migrate();
    }
} 

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();