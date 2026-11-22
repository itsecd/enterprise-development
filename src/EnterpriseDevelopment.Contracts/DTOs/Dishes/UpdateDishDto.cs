namespace EnterpriseDevelopment.Contracts.DTOs.Dishes;

/// <summary>
/// Данные для обновления блюда
/// </summary>
/// <param name="Name">Новое название блюда</param>
/// <param name="Weight">Новый вес блюда в граммах</param>
/// <param name="Price">Новая стоимость блюда</param>
/// <param name="CategoryId">Идентификатор категории блюда</param>
/// <param name="RestaurantId">Идентификатор ресторана</param>
public record UpdateDishDto(
    string Name,
    int Weight,
    decimal Price,
    int CategoryId,
    int RestaurantId);
