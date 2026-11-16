using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarWash.Infrastructure.Ef;

/// <summary>
/// Создание контекста для инструментов миграций
/// </summary>
public class CarWashDbContextFactory : IDesignTimeDbContextFactory<CarWashDbContext>
{
    /// <inheritdoc />
    public CarWashDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__carwash")
            ?? "Host=localhost;Database=carwash;Username=postgres";
        var options = new DbContextOptionsBuilder<CarWashDbContext>().UseNpgsql(connectionString).Options;
        return new CarWashDbContext(options);
    }
}
