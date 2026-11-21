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
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<bool> HasDishesAsync(int restaurantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет наличие заказов ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Признак наличия заказов</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<bool> HasOrdersAsync(int restaurantId, CancellationToken cancellationToken = default);
}
