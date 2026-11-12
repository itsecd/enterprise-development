using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Tests;

/// <summary>
/// Фикстура для предоставления тестовых данных аналитическим тестам.
/// </summary>
public class QueriesTestFixture
{
    public IReadOnlyList<Client> Clients { get; }
    public IReadOnlyList<Restaurant> Restaurants { get; }
    public IReadOnlyList<DishCategory> Categories { get; }
    public IReadOnlyList<Dish> Dishes { get; }
    public IReadOnlyList<Order> Orders { get; }

    public QueriesTestFixture()
    {
        var context = DataSeeder.Seed();

        Clients = context.Clients;
        Restaurants = context.Restaurants;
        Categories = context.Categories;
        Dishes = context.Dishes;
        Orders = context.Orders;
    }
}