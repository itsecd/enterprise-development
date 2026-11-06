using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.Categories;

/// <summary>
/// Данные для создания или изменения категории блюд
/// </summary>
public sealed class CategoryRequest
{
    /// <summary>
    /// Название категории
    /// </summary>
    [Required(ErrorMessage = "Укажите название категории.")]
    public required string Name { get; init; }
}