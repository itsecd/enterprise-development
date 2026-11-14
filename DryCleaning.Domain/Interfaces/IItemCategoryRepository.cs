namespace DryCleaning.Domain.Interfaces;

public interface IItemCategoryRepository
{
    Task<IReadOnlyList<ItemCategory>> GetAllAsync();
    Task<ItemCategory?> GetByIdAsync(int id);
    Task<ItemCategory> AddAsync(ItemCategory category);
    Task<bool> UpdateAsync(ItemCategory category);
    Task<bool> DeleteAsync(int id);
    Task<bool> HasItemsForCategoryAsync(int categoryId);
}
