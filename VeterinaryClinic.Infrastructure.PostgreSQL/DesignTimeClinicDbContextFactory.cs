using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VeterinaryClinic.Infrastructure.PostgreSQL;

public sealed class DesignTimeClinicDbContextFactory : IDesignTimeDbContextFactory<ClinicDbContext>
{
    public ClinicDbContext CreateDbContext(string[] args)
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionStrings__clinicdb")
            ?? throw new InvalidOperationException("ConnectionStrings__clinicdb is not configured.");
        return new ClinicDbContext(new DbContextOptionsBuilder<ClinicDbContext>().UseNpgsql(connection).Options);
    }
}
