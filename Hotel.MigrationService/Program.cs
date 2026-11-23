using Hotel.Infrastructure;
using Hotel.MigrationService;
using Microsoft.EntityFrameworkCore;
using Hotel.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDbContext<HotelDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("hoteldb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .UseSnakeCaseNamingConvention();
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();