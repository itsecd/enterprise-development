using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BeautySalon.Infrastructure.SqlServer;

public sealed class SalonDbContextFactory : IDesignTimeDbContextFactory<SalonDbContext>
{
    public SalonDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__salondb")
            ?? @"Server=(localdb)\MSSQLLocalDB;Database=BeautySalon;Integrated Security=true;TrustServerCertificate=true";

        var options = new DbContextOptionsBuilder<SalonDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new SalonDbContext(options);
    }
}
