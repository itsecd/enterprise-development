using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.Restaurants;

/// <summary>
/// Данные для создания или изменения ресторана
/// </summary>
public sealed class RestaurantRequest
{
    /// <summary>
    /// Название ресторана
    /// </summary>
    [Required(ErrorMessage = "Укажите название ресторана")]
    [StringLength(100)]
    public required string Name { get; init; }

    /// <summary>
    /// Адрес ресторана
    /// </summary>
    [Required(ErrorMessage = "Укажите адрес ресторана")]
    [StringLength(255)]
    public required string Address { get; init; }

    /// <summary>
    /// Рейтинг ресторана по пятибалльной шкале
    /// </summary>
    [Range(
        typeof(decimal),
        "0",
        "5",
        ParseLimitsInInvariantCulture = true,
        ErrorMessage = "Рейтинг должен находиться в диапазоне от 0 до 5")]
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
