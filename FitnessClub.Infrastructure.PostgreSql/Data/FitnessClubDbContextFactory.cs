using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessClub.Infrastructure.PostgreSql.Data;

/// <summary>
/// Design-time фабрика контекста
/// </summary>
public class FitnessClubDbContextFactory : IDesignTimeDbContextFactory<FitnessClubDbContext>
{
    /// <summary>
    /// Создаёт контекст для генерации миграций
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
    /// <returns>Контекст базы данных</returns>
    public FitnessClubDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<FitnessClubDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=fitnessclub;Username=postgres;Password=postgres")
            .Options;

        return new FitnessClubDbContext(options);
    }
}
