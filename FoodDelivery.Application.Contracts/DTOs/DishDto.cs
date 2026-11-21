namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO блюда
/// </summary>
/// <param name="Id">Уникальный идентификатор блюда</param>
/// <param name="Name">Название блюда</param>
/// <param name="WeightGrams">Вес блюда в граммах</param>
/// <param name="Price">Цена блюда</param>
/// <param name="CategoryId">Идентификатор категории блюда</param>
/// <param name="RestaurantId">Идентификатор ресторана, которому принадлежит блюдо</param>
public record DishDto(
    int Id,
    string Name,
    int WeightGrams,
    decimal Price,
    int CategoryId,
    int RestaurantId
);
