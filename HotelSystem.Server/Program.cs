using Aspire.Npgsql.EntityFrameworkCore.PostgreSQL;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;
using HotelSystem.Infrastructure.EfCore.Repositories;
using HotelSystem.Server.ExceptionHandlers;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ReferenceNotFoundExceptionHandler>();
builder.Services.AddOpenApi();

// Подключение к PostgreSQL через Aspire
builder.AddNpgsqlDbContext<HotelDbContext>("hoteldb");

// Регистрация EF Core репозиториев
builder.Services.AddScoped<IRepository<Client>, EfClientRepository>();
builder.Services.AddScoped<IRepository<RoomType>, EfRoomTypeRepository>();
builder.Services.AddScoped<IRepository<Room>, EfRoomRepository>();
builder.Services.AddScoped<IRepository<Booking>, EfBookingRepository>();

// Регистрация сервисов приложения
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<RoomTypeService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<AnalyticsService>();

var app = builder.Build();

// Применение миграций при запуске
if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();