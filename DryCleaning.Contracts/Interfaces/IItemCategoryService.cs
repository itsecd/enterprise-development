using DryCleaning.Contracts.Dto;

namespace DryCleaning.Contracts.Interfaces;

public interface IItemCategoryService
{
    Task<IReadOnlyList<ItemCategoryDto>> GetAllAsync();
    Task<ItemCategoryDto?> GetByIdAsync(int id);
    Task<ItemCategoryDto> AddAsync(ItemCategoryEditDto category);
    Task<bool> UpdateAsync(int id, ItemCategoryEditDto category);
    Task<bool> DeleteAsync(int id);
    Task<CategoryPopularityDto> GetPopularityAsync(DateTime end);
}
