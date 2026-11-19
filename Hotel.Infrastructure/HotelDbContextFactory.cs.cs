using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hotel.Infrastructure;

public class HotelDbContextFactory : IDesignTimeDbContextFactory<HotelDbContext>
{
    public HotelDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();

        optionsBuilder.UseMySql(
            "Server=localhost;Database=hoteldb;User=root;Password=design-time-only;",
            new MySqlServerVersion(new Version(9, 7, 0))
        );

        return new HotelDbContext(optionsBuilder.Options);
    }
}