using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет операции хранения блюд
/// </summary>
public interface IDishRepository : IRepository<Dish>
{
    /// <summary>
    /// Проверяет использование блюда в заказах
    /// </summary>
    /// <param name="dishId">Идентификатор блюда</param>
    /// <returns>Признак использования блюда в заказах</returns>
    bool IsUsedInOrders(int dishId);

    /// <summary>
    /// Возвращает блюда категории
    /// </summary>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <returns>Блюда категории</returns>
    IReadOnlyList<Dish> GetByCategory(int categoryId);

    /// <summary>
    /// Возвращает блюда ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Блюда ресторана</returns>
    IReadOnlyList<Dish> GetByRestaurant(int restaurantId);
}
