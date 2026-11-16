using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DryCleaning.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DryCleaningDbContext>
{
    public DryCleaningDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DryCleaningDbContext>();

        var connectionString = "Server=localhost;Database=cleaningdb;User=root;Password=admin;";

        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));

        return new DryCleaningDbContext(optionsBuilder.Options);
    }
}
