using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DryCleaning.Infrastructure.EfCore.Data;

/// <summary>
/// Создаёт контекст для команд dotnet ef (генерация миграций) без запуска API и Aspire
/// </summary>
public class DryCleaningDbContextFactory : IDesignTimeDbContextFactory<DryCleaningDbContext>
{
    /// <inheritdoc />
    public DryCleaningDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<DryCleaningDbContext>()
            .UseSqlServer("Server=localhost;Database=DryCleaning;TrustServerCertificate=True")
            .Options;

        return new DryCleaningDbContext(options);
    }
}