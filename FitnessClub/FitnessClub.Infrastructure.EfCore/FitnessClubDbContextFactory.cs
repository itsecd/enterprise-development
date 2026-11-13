using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessClub.Infrastructure.EfCore;

/// <summary>Создаёт контекст для команд миграции без запуска API.</summary>
public class FitnessClubDbContextFactory : IDesignTimeDbContextFactory<FitnessClubDbContext>
{
    /// <inheritdoc />
    public FitnessClubDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__fitnessclub")
            ?? throw new InvalidOperationException("Задайте переменную ConnectionStrings__fitnessclub для команд EF Core.");
        var options = new DbContextOptionsBuilder<FitnessClubDbContext>()
            .UseNpgsql(connectionString)
            .Options;
        return new FitnessClubDbContext(options);
    }
}
