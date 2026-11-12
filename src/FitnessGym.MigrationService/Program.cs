using FitnessGym.Infrastructure.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("fitnessgym")
                       ?? throw new InvalidOperationException("Не задана строка подключения ConnectionStrings:fitnessgym.");
builder.Services.AddDbContext<FitnessGymDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql => npgsql.EnableRetryOnFailure()));

using var host = builder.Build();
await using var scope = host.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<FitnessGymDbContext>();
await context.Database.MigrateAsync();
