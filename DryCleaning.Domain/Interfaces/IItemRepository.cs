namespace DryCleaning.Domain.Interfaces;

public interface IItemRepository
{
    Task<IReadOnlyList<Item>> GetAllAsync();
    Task<IReadOnlyList<Item>> GetByCategoryIdAsync(int categoryId);
    Task<Item?> GetByIdAsync(int id);
    Task<Item> AddAsync(Item item);
    Task<bool> UpdateAsync(Item item);
    Task<bool> DeleteAsync(int id);
    Task<bool> HasItemsForCategoryAsync(int categoryId);
}
