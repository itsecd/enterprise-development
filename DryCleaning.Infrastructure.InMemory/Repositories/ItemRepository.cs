using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Domain.Seeds;

namespace DryCleaning.Infrastructure.InMemory.Repositories;

public class ItemRepository(TestData data) : IItemRepository
{
    private readonly List<Item> _items = [.. data.Items];
    private int _nextId = 11;

    public Task<IReadOnlyList<Item>> GetAllAsync() => Task.FromResult<IReadOnlyList<Item>>(_items);
    public Task<IReadOnlyList<Item>> GetByCategoryIdAsync(int categoryId) =>
        Task.FromResult<IReadOnlyList<Item>>(_items.Where(i => i.Category.CategoryId == categoryId).ToList());
    public Task<Item?> GetByIdAsync(int id) => Task.FromResult(_items.FirstOrDefault(i => i.ItemId == id));

    public Task<Item> AddAsync(Item item)
    {
        item.ItemId = _nextId++;
        _items.Add(item);
        return Task.FromResult(item);
    }

    public Task<bool> UpdateAsync(Item item)
    {
        var index = _items.FindIndex(i => i.ItemId == item.ItemId);
        if (index == -1) return Task.FromResult(false);
        _items[index] = item;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var item = _items.FirstOrDefault(i => i.ItemId == id);
        if (item is null) return Task.FromResult(false);
        _items.Remove(item);
        return Task.FromResult(true);
    }

    public Task<bool> HasItemsForCategoryAsync(int categoryId) =>
        Task.FromResult(_items.Any(i => i.Category.CategoryId == categoryId));
}
