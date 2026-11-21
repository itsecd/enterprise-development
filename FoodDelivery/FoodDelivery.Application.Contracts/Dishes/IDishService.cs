using FoodDelivery.Application.Contracts.Common;

namespace FoodDelivery.Application.Contracts.Dishes;

/// <summary>
/// Определяет прикладные операции с блюдами
/// </summary>
public interface IDishService : ICrudService<DishDto, DishRequest>
{
    /// <summary>
    /// Возвращает блюда категории
    /// </summary>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <returns>Блюда категории</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<DishDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает блюда ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Блюда ресторана</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<DishDto>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);
}
