using FitnessClub.Infrastructure.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FitnessClub.MigrationService;

/// <summary>
/// Применяет миграции базы данных и завершает работу приложения
/// </summary>
public sealed class MigrationService(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime applicationLifetime,
    ILogger<MigrationService> logger) : BackgroundService
{
    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Начали применять миграции");
            await using var scope = serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FitnessClubDbContext>();
            await dbContext.Database.MigrateAsync(stoppingToken);
            logger.LogInformation("Миграции успешно применены");
        }
        finally
        {
            applicationLifetime.StopApplication();
        }
    }
}