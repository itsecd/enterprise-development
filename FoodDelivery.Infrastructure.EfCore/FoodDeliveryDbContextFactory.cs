using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodDelivery.Infrastructure.EfCore;

/// <summary>
/// Фабрика для создания экземпляра <see cref="FoodDeliveryDbContext"/> во время выполнения операций efcore
/// </summary>
public class FoodDeliveryDbContextFactory : IDesignTimeDbContextFactory<FoodDeliveryDbContext>
{
    /// <summary>
    /// Создаёт экземпляр контекста базы данных для операций проектирования
    /// </summary>
    public FoodDeliveryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FoodDeliveryDbContext>();

        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=food_delivery;Username=postgres;Password=postgres");

        return new FoodDeliveryDbContext(optionsBuilder.Options);
    }
}