using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TransportCompany.Infrastructure.MsSqlServer.Data;

namespace TransportCompany.Api;

/// <summary>
/// Фабрика для генерации миграций
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TransportCompanyDbContext>
{
    /// <summary>
    /// Создаёт экземпляр контекста БД
    /// </summary>
    public TransportCompanyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TransportCompanyDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TransportCompanyDesignDb;Trusted_Connection=True;");
        return new TransportCompanyDbContext(optionsBuilder.Options);
    }
}