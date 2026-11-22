namespace EnterpriseDevelopment.Contracts.DTOs.Categories;

/// <summary>
/// Данные для создания категории блюд
/// </summary>
/// <param name="Name">Название создаваемой категории</param>
public record CreateCategoryDto(
    string Name);
