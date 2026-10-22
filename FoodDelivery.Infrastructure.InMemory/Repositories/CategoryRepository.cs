using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализует репозиторий категорий
/// </summary>
public class CategoryRepository()
    : InMemoryRepository<Category>(TestData.Categories),
      ICategoryRepository
{
    /// <inheritdoc />
    public override Category? Update(Category category)
    {
        var existingCategory = GetById(category.Id);

        if (existingCategory is null)
        {
            return null;
        }

        existingCategory.Name = category.Name;
        return existingCategory;
    }
}
