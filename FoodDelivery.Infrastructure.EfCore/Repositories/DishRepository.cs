using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализует репозиторий блюд с использованием efcore
/// </summary>
public class DishRepository(FoodDeliveryDbContext dbContext)
    : EfRepository<Dish>(dbContext),
      IDishRepository
{
    /// <inheritdoc />
    public override async Task<Dish?> UpdateAsync(Dish dish)
    {
        var existingDish = await GetByIdAsync(dish.Id);

        if (existingDish is null)
        {
            return null;
        }

        existingDish.Name = dish.Name;
        existingDish.WeightGrams = dish.WeightGrams;
        existingDish.Price = dish.Price;
        existingDish.CategoryId = dish.CategoryId;
        existingDish.Category = dish.Category;
        existingDish.RestaurantId = dish.RestaurantId;
        existingDish.Restaurant = dish.Restaurant;

        await DbContext.SaveChangesAsync();

        return existingDish;
    }
}