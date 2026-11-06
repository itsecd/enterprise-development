using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет операции хранения ресторанов
/// </summary>
public interface IRestaurantRepository : IRepository<Restaurant>
{
    /// <summary>
    /// Проверяет наличие блюд ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Признак наличия блюд</returns>
    bool HasDishes(int restaurantId);

    /// <summary>
    /// Проверяет наличие заказов ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Признак наличия заказов</returns>
    bool HasOrders(int restaurantId);
}
