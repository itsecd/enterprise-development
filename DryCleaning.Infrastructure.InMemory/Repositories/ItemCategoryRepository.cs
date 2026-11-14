using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Domain.Seeds;

namespace DryCleaning.Infrastructure.InMemory.Repositories;

public class ItemCategoryRepository(TestData data) : IItemCategoryRepository
{
    private readonly List<ItemCategory> _categories = [.. data.Categories];
    private int _nextId = 11;

    public Task<IReadOnlyList<ItemCategory>> GetAllAsync() => Task.FromResult<IReadOnlyList<ItemCategory>>(_categories);
    public Task<ItemCategory?> GetByIdAsync(int id) => Task.FromResult(_categories.FirstOrDefault(c => c.CategoryId == id));

    public Task<ItemCategory> AddAsync(ItemCategory category)
    {
        category.CategoryId = _nextId++;
        _categories.Add(category);
        return Task.FromResult(category);
    }

    public Task<bool> UpdateAsync(ItemCategory category)
    {
        var index = _categories.FindIndex(c => c.CategoryId == category.CategoryId);
        if (index == -1) return Task.FromResult(false);
        _categories[index] = category;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var c = _categories.FirstOrDefault(x => x.CategoryId == id);
        if (c is null) return Task.FromResult(false);
        _categories.Remove(c);
        return Task.FromResult(true);
    }

    public Task<bool> HasItemsForCategoryAsync(int categoryId) =>
        Task.FromResult(data.Items.Any(i => i.Category.CategoryId == categoryId));
}
