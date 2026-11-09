using EnterpriseDevelopment.Contracts.DTOs.Categories;

namespace EnterpriseDevelopment.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с категориями блюд
/// </summary>
public interface ICategoryService
    : ICrudService<CategoryDto, CreateCategoryDto, UpdateCategoryDto>
{
}
