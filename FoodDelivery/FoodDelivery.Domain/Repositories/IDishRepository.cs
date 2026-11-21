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
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<bool> IsUsedInOrdersAsync(int dishId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает блюда категории
    /// </summary>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <returns>Блюда категории</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<Dish>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает блюда ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Блюда ресторана</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<Dish>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);
}
