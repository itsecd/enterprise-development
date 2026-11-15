
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarWash.Infrastructure.EfCore;

/// <summary>
/// Фабрика создания контекста базы данных
/// </summary>
public class CarWashDbContextFactory
    : IDesignTimeDbContextFactory<CarWashDbContext>
{
    public CarWashDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CarWashDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=CarWashDb;Trusted_Connection=True;TrustServerCertificate=True;");

        return new CarWashDbContext(optionsBuilder.Options);
    }
}
