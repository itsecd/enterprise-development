using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessGym.Infrastructure.PostgreSql;

/// <summary>
/// Создаёт контекст для dotnet ef без запуска сервера или оркестратора.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FitnessGymDbContext>
{
    public FitnessGymDbContext CreateDbContext(string[] args)
    {
        // Для генерации миграций соединение не открывается. Пароль хранится вне исходников.
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__fitnessgym")
                               ?? "Host=localhost;Database=fitnessgym;Username=postgres";
        return new FitnessGymDbContext(new DbContextOptionsBuilder<FitnessGymDbContext>()
            .UseNpgsql(connectionString).Options);
    }
}
