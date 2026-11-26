using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VetClinic.EfCore;

/// <summary>
/// Фабрика для создания экземпляра <see cref="VetClinicDbContext"/> во время разработки (Design-Time).
/// </summary>
public class VetClinicDbContextFactory : IDesignTimeDbContextFactory<VetClinicDbContext>
{
    ///<inheritdoc/>
    public VetClinicDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VetClinicDbContext>();

        optionsBuilder.UseMySql(
            "Server=localhost;Database=vetclinicdb;User=root;Password=design-time-only;",
            new MySqlServerVersion(new Version(9, 7, 0))
        );

        return new VetClinicDbContext(optionsBuilder.Options);
    }
}
