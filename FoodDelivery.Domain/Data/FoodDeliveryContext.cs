using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Data;

/// <summary>
/// Контекст хранения коллекций сущностей в оперативной памяти.
/// </summary>
public class FoodDeliveryContext
{
    public List<Client> Clients { get; set; } = [];
    public List<Restaurant> Restaurants { get; set; } = [];
    public List<DishCategory> Categories { get; set; } = [];
    public List<Dish> Dishes { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
}