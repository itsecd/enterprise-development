using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Фабрика контекста базы данных для создания миграций
/// </summary>
public class ApplicationDbContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <summary>
    /// Создаёт контекст базы данных для инструментов EF Core
    /// </summary>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(
                "Host=localhost;Database=fooddeliverydb;Username=postgres;Password=design-time-only")
            .Options;

        return new ApplicationDbContext(options);
    }
}
