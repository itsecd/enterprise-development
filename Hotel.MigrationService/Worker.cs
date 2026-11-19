using Hotel.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Hotel.MigrationService;

/// <summary>
/// Воркер выполнящий миграцию ДБ
/// </summary>
public class Worker(
    ILogger<Worker> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Выполняю миграции...");

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

            await dbContext.Database.MigrateAsync(stoppingToken);

            logger.LogInformation("Миграции успешно применены!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Произошла ошибка при применении миграций.");
            throw;
        }
        finally
        {
            hostApplicationLifetime.StopApplication();
        }
    }
}