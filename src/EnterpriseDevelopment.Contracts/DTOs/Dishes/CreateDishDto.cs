namespace EnterpriseDevelopment.Contracts.DTOs.Dishes;

/// <summary>
/// Данные для создания блюда
/// </summary>
/// <param name="Name">Название создаваемого блюда</param>
/// <param name="Weight">Вес блюда в граммах</param>
/// <param name="Price">Стоимость блюда</param>
/// <param name="CategoryId">Идентификатор категории блюда</param>
/// <param name="RestaurantId">Идентификатор ресторана</param>
public record CreateDishDto(
    string Name,
    int Weight,
    decimal Price,
    int CategoryId,
    int RestaurantId);
