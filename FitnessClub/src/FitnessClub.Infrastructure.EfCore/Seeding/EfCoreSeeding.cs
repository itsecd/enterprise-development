using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Infrastructure.EfCore.Seeding;

/// <summary>
/// Настройка начального заполнения базы данных
/// </summary>
public static class EfCoreSeeding
{
    /// <summary>
    /// Добавляет асинхронное заполнение созданной базы данных из <see cref="SeedData"/>
    /// </summary>
    /// <param name="options">Построитель настроек контекста базы данных</param>
    /// <returns>Построитель настроек для продолжения конфигурации</returns>
    public static DbContextOptionsBuilder UseFitnessClubSeeding(this DbContextOptionsBuilder options)
    {
        options.UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            var logger = context.GetService<ILoggerFactory>()
                .CreateLogger("FitnessClub.Database.Seeding");
            if (await context.Set<Specialization>().AnyAsync(cancellationToken))
            {
                logger.LogInformation("Заполнение базы данных пропущено: данные уже существуют");
                return;
            }
            logger.LogInformation("Начато заполнение базы данных");
            var seedData = new SeedData();
            context.AddRange(seedData.Specializations);
            context.AddRange(seedData.Halls);
            context.AddRange(seedData.Trainers);
            context.AddRange(seedData.Clients);
            context.AddRange(seedData.Bookings);

            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Заполнение базы данных завершено");
        });
        return options;
    }
}