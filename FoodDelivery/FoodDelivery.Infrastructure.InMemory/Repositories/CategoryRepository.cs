using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранит категории блюд в памяти
/// </summary>
/// <param name="data">Данные приложения</param>
public sealed class CategoryRepository(FoodDeliveryData data)
    : InMemoryRepository<Category>(
        data.Categories,
        category => category.Id,
        InMemorySynchronization.For(data)),
        ICategoryRepository
{
    /// <inheritdoc/>
    public bool HasDishes(int categoryId)
    {
        return this.Execute<bool>(() =>
        {
            return data.Dishes.Any(dish => dish.Category.Id == categoryId);
        });
    }

    /// <inheritdoc/>
    protected override void SetId(Category entity, int id)
    {
        entity.Id = id;
    }

    /// <inheritdoc/>
    protected override void UpdateEntity(
        Category existingEntity,
        Category updatedEntity)
    {
        existingEntity.Name = updatedEntity.Name;
    }
}
