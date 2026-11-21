using FitnessClub.Infrastructure.EfCore.Persistence;
using FitnessClub.Infrastructure.EfCore.Seeding;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FitnessClub.MigrationService;


var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddMySqlDbContext<FitnessClubDbContext>(
    "fitnessclub-db",
    configureDbContextOptions: options =>
        options.UseFitnessClubSeeding());

builder.Services.AddHostedService<MigrationService>();

var host = builder.Build();
await host.RunAsync();
