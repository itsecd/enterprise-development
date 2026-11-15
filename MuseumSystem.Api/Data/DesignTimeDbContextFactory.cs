using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MuseumSystem.Api.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MuseumDbContext>
{
    public MuseumDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MuseumDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=museumdb;Username=postgres;Password=postgres")
                      .UseSnakeCaseNamingConvention();

        return new MuseumDbContext(optionsBuilder.Options);
    }
}