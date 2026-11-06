using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранит блюда в памяти
/// </summary>
/// <param name="data">Данные приложения</param>
public sealed class DishRepository(FoodDeliveryData data)
    : InMemoryRepository<Dish>(
        data.Dishes,
        dish => dish.Id,
        InMemorySynchronization.For(data)),
        IDishRepository
{
    /// <inheritdoc/>
    public bool IsUsedInOrders(int dishId)
    {
        return this.Execute<bool>(() =>
        {
            return data.Orders.Any(
                order => order.Items.Any(item => item.Dish.Id == dishId));
        });
    }

    /// <inheritdoc/>
    protected override void SetId(Dish entity, int id)
    {
        entity.Id = id;
    }

    /// <inheritdoc/>
    protected override void UpdateEntity(
        Dish existingEntity,
        Dish updatedEntity)
    {
        existingEntity.Name = updatedEntity.Name;
        existingEntity.Weight = updatedEntity.Weight;
        existingEntity.Price = updatedEntity.Price;
        existingEntity.Category = updatedEntity.Category;
        existingEntity.Restaurant = updatedEntity.Restaurant;
    }

    /// <inheritdoc/>
    public IReadOnlyList<Dish> GetByCategory(int categoryId)
    {
        lock (SyncRoot)
        {
            return data.Dishes
                .Where(dish => dish.Category.Id == categoryId)
                .OrderBy(dish => dish.Id)
                .ToArray();
        }
    }

    /// <inheritdoc/>
    public IReadOnlyList<Dish> GetByRestaurant(int restaurantId)
    {
        lock (SyncRoot)
        {
            return data.Dishes
                .Where(dish => dish.Restaurant.Id == restaurantId)
                .OrderBy(dish => dish.Id)
                .ToArray();
        }
    }
}
