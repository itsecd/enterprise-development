using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using VetClinic.Application;
using VetClinic.EfCore;
using VetClinic.ServiceDefaults;
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi();

// Подключаем MySQL + EF Core через клиентскую интеграцию Aspire
builder.AddEfCoreDataLayer("vetclinicdb");
builder.Services.AddApplicationLayer();

var app = builder.Build();

// Автоматически создаем таблицы и заливаем данные из HasData в контейнер MySQL
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VetClinicDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("VetClinic API (MySQL + Aspire)")
               .WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
