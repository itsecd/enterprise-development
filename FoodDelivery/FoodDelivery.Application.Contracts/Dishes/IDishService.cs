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
    IReadOnlyList<DishDto> GetByCategory(int categoryId);

    /// <summary>
    /// Возвращает блюда ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Блюда ресторана</returns>
    IReadOnlyList<DishDto> GetByRestaurant(int restaurantId);
}
