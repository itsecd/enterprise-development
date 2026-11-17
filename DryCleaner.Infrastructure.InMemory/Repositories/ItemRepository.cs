using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория для работы с изделиями в памяти
/// </summary>
public class ItemRepository()
    : InMemoryRepository<Item>(DataSeed.Items.ToList())
{
    /// <inheritdoc />
    public override Task<Item?> UpdateAsync(Item entity)
    {
        var existing = GetByIdAsync(entity.Id).Result;
        if (existing is null) return Task.FromResult<Item?>(null);

        existing.Name = entity.Name;
        existing.CategoryId = entity.CategoryId;
        existing.Category = entity.Category;
        existing.Material = entity.Material;

        return Task.FromResult<Item?>(existing);
    }
}