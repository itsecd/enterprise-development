using Hotel.Application.Contracts.Bookings;
using Hotel.Application.Contracts.HotelClients;
using Hotel.Application.Contracts.Rooms;
using Hotel.Application.Contracts.RoomTypes;
using Hotel.Application.Services;
using Hotel.Domain;
using Hotel.Domain.Entities;
using Hotel.Infrastructure.EfCore;
using Hotel.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

//Настройка подключения к MySQL
var connectionString = builder.Configuration.GetConnectionString("HotelDb")
    ?? throw new InvalidOperationException("Не задана строка подключения");

builder.Services.AddDbContext<HotelDbContext>(options => options.UseMySQL(connectionString));

// Регистрация репозиториев
builder.Services.AddScoped<IRepository<RoomType, int>, RoomTypeRepository>();
builder.Services.AddScoped<IRepository<Room, int>, RoomRepository>();
builder.Services.AddScoped<IRepository<HotelClient, int>, HotelClientRepository>();
builder.Services.AddScoped<IRepository<Booking, int>, BookingRepository>();

// Регистрация сервисов приложения
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IHotelClientService, HotelClientService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// Регистрация контроллеров и OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
    await db.Database.MigrateAsync();

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapDefaultEndpoints();

app.Run();
