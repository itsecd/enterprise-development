using Museum.Application.Services;
using Museum.Application.Repositories;
using Museum.Domain.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<MuseumContext>(_ => DataSeeder.Seed());
builder.Services.AddScoped<MuseumAnalyticsService>();
builder.Services.AddScoped<ExhibitionsRepository>();
builder.Services.AddScoped<ToursRepository>();
builder.Services.AddScoped<VisitorsRepository>();
builder.Services.AddScoped<TicketsRepository>();
builder.Services.AddScoped<TourExhibitionsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Museum API is running");

app.Run();
