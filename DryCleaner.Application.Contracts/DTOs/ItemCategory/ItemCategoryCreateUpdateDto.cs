using System.ComponentModel.DataAnnotations;
using DryCleaner.Shared.Enums;

namespace DryCleaner.Application.Contracts.DTOs.ItemCategory;

/// <summary>
/// DTO для создания или обновления категории изделия
/// </summary>
/// <param name="Name">Название категории</param>
/// <param name="RecommendedCleaning">Рекомендуемый вид чистки</param>
/// <param name="Price">Стоимость обработки</param>
public record ItemCategoryCreateUpdateDto(
    [Required(ErrorMessage = "Название категории обязательно для заполнения!")]
    string Name,

    CleaningType RecommendedCleaning,

    [Range(0, int.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
    decimal Price
);