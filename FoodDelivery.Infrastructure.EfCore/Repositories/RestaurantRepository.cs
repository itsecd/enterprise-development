using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализует репозиторий ресторанов с использованием efcore
/// </summary>
public class RestaurantRepository(FoodDeliveryDbContext dbContext)
    : EfRepository<Restaurant>(dbContext),
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

        await DbContext.SaveChangesAsync();

        return existingRestaurant;
    }
}