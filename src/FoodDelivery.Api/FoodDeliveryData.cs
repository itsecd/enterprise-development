using FoodDelivery.Domain;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Api;

/// <summary>
/// Данные приложения, хранящиеся в памяти.
/// </summary>
public class FoodDeliveryData
{
    /// <summary>
    /// Клиенты.
    /// </summary>
    public List<Client> Clients { get; }
    /// <summary>
    /// Рестораны.
    /// </summary>
    public List<Restaurant> Restaurants { get; }
    /// <summary>
    /// Категории блюд.
    /// </summary>
    public List<DishCategory> Categories { get; }
    /// <summary>
    /// Блюда.
    /// </summary>
    public List<Dish> Dishes { get; }
    /// <summary>
    /// Заказы.
    /// </summary>
    public List<Order> Orders { get; }
    /// <summary>
    /// Позиции заказов.
    /// </summary>
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
