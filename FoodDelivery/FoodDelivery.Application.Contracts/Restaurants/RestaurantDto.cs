namespace FoodDelivery.Application.Contracts.Restaurants;

/// <summary>
/// Сведения о ресторане
/// </summary>
public sealed class RestaurantDto
{
    /// <summary>
    /// Идентификатор ресторана
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название ресторана
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Адрес ресторана
    /// </summary>
    public required string Address { get; init; }

    /// <summary>
    /// Рейтинг ресторана по пятибалльной шкале
    /// </summary>
    public required decimal Rating { get; init; }

    /// <summary>
    /// Время открытия ресторана
    /// </summary>
    public required TimeOnly OpeningTime { get; init; }

    /// <summary>
    /// Время закрытия ресторана
    /// </summary>
    public required TimeOnly ClosingTime { get; init; }
}
