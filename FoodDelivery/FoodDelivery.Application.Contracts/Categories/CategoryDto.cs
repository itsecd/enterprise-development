namespace FoodDelivery.Application.Contracts.Categories;

/// <summary>
/// Сведения о категории блюд
/// </summary>
public sealed class CategoryDto
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; init; }
}