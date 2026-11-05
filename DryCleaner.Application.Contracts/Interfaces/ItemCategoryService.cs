using DryCleaner.Application.Contracts.DTOs.ItemCategory;

namespace DryCleaner.Application.Contracts.Interfaces;

/// <summary>
/// Определяет стандартные операции CRUD для работы с категориями изделий
/// </summary>
public interface IItemCategoryService
    : IApplicationService<ItemCategoryDto, ItemCategoryCreateUpdateDto, int>
{
}