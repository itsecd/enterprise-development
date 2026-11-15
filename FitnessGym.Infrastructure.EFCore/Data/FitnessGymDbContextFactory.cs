using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessGym.Infrastructure.EFCore.Data;

public class FitnessGymDbContextFactory : IDesignTimeDbContextFactory<FitnessGymDbContext>
{
    public FitnessGymDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FitnessGymDbContext>();

        optionsBuilder.UseMySql(
            "Server=localhost;Database=FitnessGym;User=root;Password=root;",
            ServerVersion.Parse("8.0.32-mysql"));

        return new FitnessGymDbContext(optionsBuilder.Options);
    }
}