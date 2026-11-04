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
    public override Item? Update(Item entity)
    {
        var existing = GetById(entity.Id);
        if (existing is null)
        {
            return null;
        }

        existing.Name = entity.Name;
        existing.CategoryId = entity.CategoryId;
        existing.Category = entity.Category;
        existing.Material = entity.Material;

        return existing;
    }
}