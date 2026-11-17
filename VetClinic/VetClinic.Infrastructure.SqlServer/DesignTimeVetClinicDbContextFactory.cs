using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VetClinic.Infrastructure.SqlServer;

/// <summary>
/// Создает контекст для dotnet ef при генерации миграций, к базе при этом не подключается
/// </summary>
public class DesignTimeVetClinicDbContextFactory : IDesignTimeDbContextFactory<VetClinicDbContext>
{
    /// <inheritdoc/>
    public VetClinicDbContext CreateDbContext(string[] args) =>
        new(new DbContextOptionsBuilder<VetClinicDbContext>().UseSqlServer().Options);
}
