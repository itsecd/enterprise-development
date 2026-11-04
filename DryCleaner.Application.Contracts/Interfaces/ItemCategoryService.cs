using DryCleaner.Application.Contracts.DTOs.ItemCategory;

namespace DryCleaner.Application.Contracts.Interfaces;

public interface IItemCategoryService
    : IApplicationService<ItemCategoryDto, ItemCategoryCreateUpdateDto, int>
{
}