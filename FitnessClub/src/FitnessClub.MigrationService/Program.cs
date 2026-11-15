using FitnessClub.Infrastructure.EfCore.Persistence;
using FitnessClub.Infrastructure.EfCore.Seeding;
using FitnessClub.Infrastructure.EfCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FitnessClub.MigrationService;


var builder = Host.CreateApplicationBuilder(args);

builder.AddMySqlDbContext<FitnessClubDbContext>(
    "fitnessclub-db",
    configureDbContextOptions: options =>
        options.UseFitnessClubSeeding());

builder.Services.AddEfCoreStorage();
builder.Services.AddHostedService<MigrationService>();

var host = builder.Build();
await host.RunAsync();
