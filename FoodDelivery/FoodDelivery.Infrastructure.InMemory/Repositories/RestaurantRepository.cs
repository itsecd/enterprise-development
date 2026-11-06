using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранит рестораны в памяти
/// </summary>
/// <param name="data">Данные приложения</param>
public sealed class RestaurantRepository(FoodDeliveryData data)
    : InMemoryRepository<Restaurant>(
        data.Restaurants,
        restaurant => restaurant.Id,
        InMemorySynchronization.For(data)),
        IRestaurantRepository
{
    /// <inheritdoc/>
    public bool HasDishes(int restaurantId)
    {
        return this.Execute<bool>(() =>
        {
            return data.Dishes.Any(dish => dish.Restaurant.Id == restaurantId);
        });
    }

    /// <inheritdoc/>
    public bool HasOrders(int restaurantId)
    {
        return this.Execute<bool>(() =>
        {
            return data.Orders.Any(order => order.Restaurant.Id == restaurantId);
        });
    }

    /// <inheritdoc/>
    protected override void SetId(Restaurant entity, int id)
    {
        entity.Id = id;
    }

    /// <inheritdoc/>
    protected override void UpdateEntity(
        Restaurant existingEntity,
        Restaurant updatedEntity)
    {
        existingEntity.Name = updatedEntity.Name;
        existingEntity.Address = updatedEntity.Address;
        existingEntity.Rating = updatedEntity.Rating;
        existingEntity.OpeningTime = updatedEntity.OpeningTime;
        existingEntity.ClosingTime = updatedEntity.ClosingTime;
    }
}
