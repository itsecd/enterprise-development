using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализует репозиторий ресторанов
/// </summary>
public class RestaurantRepository()
    : InMemoryRepository<Restaurant>(TestData.Restaurants),
      IRestaurantRepository
{
    /// <inheritdoc />
    public override async Task<Restaurant?> UpdateAsync(
        Restaurant restaurant)
    {
        var existingRestaurant =
            await GetByIdAsync(restaurant.Id);

        if (existingRestaurant is null)
        {
            return null;
        }

        existingRestaurant.Name = restaurant.Name;
        existingRestaurant.Address = restaurant.Address;
        existingRestaurant.Rating = restaurant.Rating;
        existingRestaurant.OpeningTime = restaurant.OpeningTime;
        existingRestaurant.ClosingTime = restaurant.ClosingTime;

        return existingRestaurant;
    }
}