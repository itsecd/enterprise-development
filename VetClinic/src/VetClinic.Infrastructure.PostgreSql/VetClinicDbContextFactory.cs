using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VetClinic.Infrastructure.PostgreSql;

public sealed class VetClinicDbContextFactory : IDesignTimeDbContextFactory<VetClinicDbContext>
{
    public VetClinicDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__vetclinic");
        var options = new DbContextOptionsBuilder<VetClinicDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new VetClinicDbContext(options);
    }
}
