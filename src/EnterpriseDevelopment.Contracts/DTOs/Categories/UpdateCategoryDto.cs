namespace EnterpriseDevelopment.Contracts.DTOs.Categories;

/// <summary>
/// Данные для обновления категории блюд
/// </summary>
/// <param name="Name">Новое название категории</param>
public record UpdateCategoryDto(
    string Name);
