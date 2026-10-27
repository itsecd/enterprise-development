using Hotel.Application.Contracts.Bookings;
using Hotel.Application.Contracts.HotelClients;
using Hotel.Application.Contracts.Rooms;
using Hotel.Application.Contracts.RoomTypes;
using Hotel.Application.Services;
using Hotel.Domain;
using Hotel.Domain.Entities;
using Hotel.Infrastructure.InMemory.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрация репозиториев
builder.Services.AddSingleton<IRepository<RoomType, int>, RoomTypeRepository>();
builder.Services.AddSingleton<IRepository<Room, int>, RoomRepository>();
builder.Services.AddSingleton<IRepository<HotelClient, int>, HotelClientRepository>();
builder.Services.AddSingleton<IRepository<Booking, int>, BookingRepository>();

// Регистрация сервисов
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
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
