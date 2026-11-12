using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Application.Contracts.DTOs.ItemCategory;
using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Order;
using DryCleaner.Application.Contracts.Interfaces;
using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Services;

/// <summary>
/// Прикладные операции над изделиями
/// </summary>
public class ItemService(IRepository<Item> itemRepository, IRepository<ItemCategory> categoryRepository) : IItemService
{
    /// <inheritdoc />
    public ItemDto Create(ItemCreateUpdateDto dto)
    {
        var category = categoryRepository.GetById(dto.CategoryId) ?? throw new KeyNotFoundException($"Категория с идентификатором {dto.CategoryId} не найдена");

        var item = new Item
        {
            Id = 0,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Category = category,
            Material = dto.Material
        };

        var created = itemRepository.Add(item);

        return new ItemDto(
            created.Id,
            created.Name,
            new ItemCategoryDto(created.Category.Id, created.Category.Name, created.Category.RecommendedCleaning, created.Category.Price),
            created.CategoryId,
            created.Material);
    }

    /// <inheritdoc />
    public ItemDto? Get(int id)
    {
        var item = itemRepository.GetById(id);
        if (item is null) return null;

        return new ItemDto(
            item.Id,
            item.Name,
            new ItemCategoryDto(item.Category.Id, item.Category.Name, item.Category.RecommendedCleaning, item.Category.Price),
            item.CategoryId,
            item.Material);
    }

    /// <inheritdoc />
    public IList<ItemDto> GetByCategoryId(int categoryId)
    {
        return itemRepository.GetAll()
            .Where(i => i.CategoryId == categoryId)
            .Select(i => new ItemDto(
                i.Id,
                i.Name,
                new ItemCategoryDto(i.Category.Id, i.Category.Name, i.Category.RecommendedCleaning, i.Category.Price),
                i.CategoryId,
                i.Material))
            .ToList();
    }

    /// <inheritdoc />
    public IList<ItemDto> GetAll()
    {
        return itemRepository.GetAll()
            .Select(i => new ItemDto(
                i.Id,
                i.Name,
                new ItemCategoryDto(i.Category.Id, i.Category.Name, i.Category.RecommendedCleaning, i.Category.Price),
                i.CategoryId,
                i.Material))
            .ToList();
    }

    /// <inheritdoc />
    public ItemDto Update(ItemCreateUpdateDto dto, int id)
    {
        var category = categoryRepository.GetById(dto.CategoryId) ?? throw new KeyNotFoundException($"Категория с идентификатором {dto.CategoryId} не найдена");

        var item = new Item
        {
            Id = id,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Category = category,
            Material = dto.Material
        };

        var updated = itemRepository.Update(item);
        if (updated is null)
        {
            throw new KeyNotFoundException($"Изделие с идентификатором {id} не найдено");
        }

        return new ItemDto(
            updated.Id,
            updated.Name,
            new ItemCategoryDto(updated.Category.Id, updated.Category.Name, updated.Category.RecommendedCleaning, updated.Category.Price),
            updated.CategoryId,
            updated.Material);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        return itemRepository.Delete(id);
    }
}