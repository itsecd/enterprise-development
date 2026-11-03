using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Repositories;
using HotelSystem.Infrastructure.Storage;
using HotelSystem.Server.ExceptionHandlers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ReferenceNotFoundExceptionHandler>();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<HotelInMemoryStorage>();

builder.Services.AddSingleton<IRepository<Client>, InMemoryClientRepository>();
builder.Services.AddSingleton<IRepository<RoomType>, InMemoryRoomTypeRepository>();
builder.Services.AddSingleton<IRepository<Room>, InMemoryRoomRepository>();
builder.Services.AddSingleton<IRepository<Booking>, InMemoryBookingRepository>();

builder.Services.AddSingleton<ClientService>();
builder.Services.AddSingleton<RoomTypeService>();
builder.Services.AddSingleton<RoomService>();
builder.Services.AddSingleton<BookingService>();
builder.Services.AddSingleton<AnalyticsService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();