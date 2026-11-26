using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessClub.Infrastructure.EfCore.Persistence;

/// <summary>
/// Создаёт контекст для EF Core Tools без запуска приложения и подключения к базе данных
/// </summary>
public sealed class FitnessClubDbContextFactory : IDesignTimeDbContextFactory<FitnessClubDbContext>
{
    /// <inheritdoc />
    public FitnessClubDbContext CreateDbContext(string[] args)
    {
        const string connectionString =
            "Server=localhost;Database=fitnessclub-design;User Id=root;Password=root;";

        var options = new DbContextOptionsBuilder<FitnessClubDbContext>()
            .UseMySql(connectionString, new MySqlServerVersion(new Version(9, 7, 0)))
            .Options;

        return new FitnessClubDbContext(options);
    }
}
