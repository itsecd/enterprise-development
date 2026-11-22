namespace EnterpriseDevelopment.Contracts.DTOs.Categories;

/// <summary>
/// Данные категории блюд
/// </summary>
/// <param name="Id">Идентификатор категории</param>
/// <param name="Name">Название категории</param>
public record CategoryDto(
    int Id,
    string Name);
