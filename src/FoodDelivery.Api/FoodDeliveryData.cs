using FoodDelivery.Domain;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// Данные приложения, хранящиеся в памяти.
/// </summary>
public class FoodDeliveryData
{
    public List<Client> Clients { get; }
    public List<Restaurant> Restaurants { get; }
    public List<DishCategory> Categories { get; }
    public List<Dish> Dishes { get; }
    public List<Order> Orders { get; }
    public List<OrderItem> OrderItems { get; }

    /// <summary>
    /// Объект для согласованного доступа к коллекциям.
    /// </summary>
    public object SyncRoot { get; } = new();

    /// <summary>
    /// Заполняет коллекции начальными данными.
    /// </summary>
    public FoodDeliveryData()
    {
        var seed = new DataSeeder();
        Clients = seed.Clients.ToList();
        Restaurants = seed.Restaurants.ToList();
        Categories = seed.DishCategories.ToList();
        Dishes = seed.Dishes.ToList();
        Orders = seed.Orders.ToList();
        OrderItems = seed.OrderItems.ToList();
    }
}
