using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarWash.Infrastructure.EfCore;

/// <summary>
/// Фабрика контекста для утилиты dotnet ef (создание миграций без запущенной БД)
/// </summary>
public class CarWashDbContextFactory : IDesignTimeDbContextFactory<CarWashDbContext>
{
    /// <inheritdoc />
    public CarWashDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<CarWashDbContext>()
            .UseMySql("Server=localhost;Database=carwashdb;User=root;Password=design-time", CarWashDbContext.MySqlVersion)
            .Options;

        return new CarWashDbContext(options);
    }
}
