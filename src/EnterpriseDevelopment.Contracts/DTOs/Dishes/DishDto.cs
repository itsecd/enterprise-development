namespace EnterpriseDevelopment.Contracts.DTOs.Dishes;

/// <summary>
/// Данные блюда
/// </summary>
/// <param name="Id">Идентификатор блюда</param>
/// <param name="Name">Название блюда</param>
/// <param name="Weight">Вес блюда в граммах</param>
/// <param name="Price">Стоимость блюда</param>
/// <param name="CategoryId">Идентификатор категории блюда</param>
/// <param name="RestaurantId">Идентификатор ресторана</param>
public record DishDto(
    int Id,
    string Name,
    int Weight,
    decimal Price,
    int CategoryId,
    int RestaurantId);
