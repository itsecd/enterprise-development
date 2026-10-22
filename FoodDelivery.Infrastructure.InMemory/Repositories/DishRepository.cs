using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализует репозиторий блюд
/// </summary>
public class DishRepository()
    : InMemoryRepository<Dish>(TestData.Dishes),
      IDishRepository
{
    /// <inheritdoc />
    public override Dish? Update(Dish dish)
    {
        var existingDish = GetById(dish.Id);

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

        return existingDish;
    }
}
