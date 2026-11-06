namespace FoodDelivery.Application.Contracts.Dishes;

/// <summary>
/// Сведения о блюде
/// </summary>
public sealed class DishDto
{
    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название блюда
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Вес блюда в граммах
    /// </summary>
    public required int Weight { get; init; }

    /// <summary>
    /// Текущая цена блюда
    /// </summary>
    public required decimal Price { get; init; }

    /// <summary>
    /// Идентификатор категории блюда
    /// </summary>
    public required int CategoryId { get; init; }

    /// <summary>
    /// Идентификатор ресторана
    /// </summary>
    public required int RestaurantId { get; init; }
}
