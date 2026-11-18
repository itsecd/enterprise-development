using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.Dishes;

/// <summary>
/// Данные для создания или изменения блюда
/// </summary>
public sealed class DishRequest : IValidatableObject
{
    /// <summary>
    /// Название блюда
    /// </summary>
    [Required(ErrorMessage = "Укажите название блюда")]
    public required string Name { get; init; }

    /// <summary>
    /// Вес блюда в граммах
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Вес должен быть положительным")]
    public required int Weight { get; init; }

    /// <summary>
    /// Цена блюда, которая должна быть больше нуля
    /// </summary>
    public required decimal Price { get; init; }

    /// <summary>
    /// Идентификатор категории блюда
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Укажите положительный идентификатор категории")]
    public required int CategoryId { get; init; }

    /// <summary>
    /// Идентификатор ресторана
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Укажите положительный идентификатор ресторана")]
    public required int RestaurantId { get; init; }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (Price <= 0)
        {
            yield return new ValidationResult(
                "Цена блюда должна быть больше нуля",
                [nameof(Price)]);
        }
    }
}
