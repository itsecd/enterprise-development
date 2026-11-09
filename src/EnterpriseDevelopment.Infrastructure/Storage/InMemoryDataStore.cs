using EnterpriseDevelopment.Domain;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Infrastructure.Storage;

/// <summary>
/// Хранилище данных службы доставки еды в оперативной памяти
/// </summary>
public class InMemoryDataStore(DataSeeder seeder)
{
    /// <summary>
    /// Коллекция клиентов
    /// </summary>
    public List<Client> Clients { get; } = seeder.Clients.ToList();

    /// <summary>
    /// Коллекция ресторанов
    /// </summary>
    public List<Restaurant> Restaurants { get; } = seeder.Restaurants.ToList();

    /// <summary>
    /// Коллекция категорий блюд
    /// </summary>
    public List<DishCategory> Categories { get; } = seeder.Categories.ToList();

    /// <summary>
    /// Коллекция блюд
    /// </summary>
    public List<Dish> Dishes { get; } = seeder.Dishes.ToList();

    /// <summary>
    /// Коллекция заказов
    /// </summary>
    public List<Order> Orders { get; } = seeder.Orders.ToList();
}
