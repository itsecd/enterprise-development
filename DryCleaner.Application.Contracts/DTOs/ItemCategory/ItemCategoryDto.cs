using DryCleaner.Shared.Enums;

namespace DryCleaner.Application.Contracts.DTOs.ItemCategory;

/// <summary>
/// DTO категории изделия
/// </summary>
/// <param name="Id">Идентификатор категории изделия</param>
/// <param name="Name">Название категории изделия</param>
/// <param name="RecommendedCleaning">Рекомендованный вид чистки</param>
/// <param name="Price">Стоимость чистки</param>
public record ItemCategoryDto(
    int Id,
    string Name,
    CleaningType RecommendedCleaning,
    decimal Price
);