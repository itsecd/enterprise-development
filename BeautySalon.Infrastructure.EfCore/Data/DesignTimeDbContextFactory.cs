using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BeautySalon.Infrastructure.EfCore.Data;

/// <summary>
/// Фабрика для создания DbContext во время выполнения команд миграции.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BeautySalonDbContext>
{
    public BeautySalonDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BeautySalonDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=beautysalondb;Trusted_Connection=True;");
        
        return new BeautySalonDbContext(optionsBuilder.Options);
    }
}