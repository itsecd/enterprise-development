using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория для работы с категориями изделий в памяти
/// </summary>
public class ItemCategoryRepository()
    : InMemoryRepository<ItemCategory>(DataSeed.Categories.ToList())
{
    /// <inheritdoc />
    public override ItemCategory? Update(ItemCategory entity)
    {
        var existing = GetById(entity.Id);
        if (existing is null)
        {
            return null;
        }

        existing.Name = entity.Name;
        existing.RecommendedCleaning = entity.RecommendedCleaning;
        existing.Price = entity.Price;

        return existing;
    }
}