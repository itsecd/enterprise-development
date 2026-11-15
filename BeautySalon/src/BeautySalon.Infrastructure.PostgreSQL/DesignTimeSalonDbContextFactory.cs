using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BeautySalon.Infrastructure.PostgreSQL;

/// <summary>
/// Позволяет инструментам миграций построить модель без запуска API и Aspire
/// </summary>
public class DesignTimeSalonDbContextFactory : IDesignTimeDbContextFactory<SalonDbContext>
{
    /// <inheritdoc />
    public SalonDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<SalonDbContext>().UseNpgsql().Options;
        return new(options);
    }
}