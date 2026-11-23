using Hotel.Application.Contracts;
using Hotel.Application.Contracts.Booking;
using Hotel.Application.Contracts.Client;
using Hotel.Application.Contracts.Room;
using Hotel.Application.Contracts.RoomType;
using Hotel.Application.Services;
using Hotel.Domain;
using Hotel.Infrastructure;
using Hotel.ServiceDefaults;
using Hotel.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddDbContext<HotelDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("hoteldb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    .UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Hotel API v1");
});

app.MapControllers();
app.MapDefaultEndpoints();

app.Run();