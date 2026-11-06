using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет операции хранения категорий блюд
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    /// <summary>
    /// Проверяет наличие блюд в категории
    /// </summary>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <returns>Признак наличия блюд в категории</returns>
    bool HasDishes(int categoryId);
}
