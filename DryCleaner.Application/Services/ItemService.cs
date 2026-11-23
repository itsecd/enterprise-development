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
    public async Task<ItemDto> CreateAsync(ItemCreateUpdateDto dto)
    {
        var category = await categoryRepository.GetByIdAsync(dto.CategoryId) ?? throw new KeyNotFoundException($"Категория с идентификатором {dto.CategoryId} не найдена");

        var item = new Item
        {
            Id = 0,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Category = category,
            Material = dto.Material
        };

        var created = await itemRepository.AddAsync(item);

        return new ItemDto(
            created.Id,
            created.Name,
            new ItemCategoryDto(created.Category.Id, created.Category.Name, created.Category.RecommendedCleaning, created.Category.Price),
            created.CategoryId,
            created.Material);
    }

    /// <inheritdoc />
    public async Task<ItemDto?> GetAsync(int id)
    {
        var item = await itemRepository.GetByIdAsync(id);
        if (item is null) return null;

        return new ItemDto(
            item.Id,
            item.Name,
            new ItemCategoryDto(item.Category.Id, item.Category.Name, item.Category.RecommendedCleaning, item.Category.Price),
            item.CategoryId,
            item.Material);
    }

    /// <inheritdoc />
    public async Task<IList<ItemDto>> GetByCategoryIdAsync(int categoryId)
    {
        var items = await itemRepository.GetAllAsync();
        return items.Where(i => i.CategoryId == categoryId).Select(MapToDto).ToList();
    }

    /// <inheritdoc />
    public async Task<IList<ItemDto>> GetAllAsync()
    {
        var items = await itemRepository.GetAllAsync();
        return items.Select(MapToDto).ToList();
    }

    /// <inheritdoc />
    public async Task<ItemDto?> UpdateAsync(ItemCreateUpdateDto dto, int id)
    {
        var category = await categoryRepository.GetByIdAsync(dto.CategoryId) ?? throw new KeyNotFoundException($"Категория с идентификатором {dto.CategoryId} не найдена");

        var item = new Item
        {
            Id = id,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Category = category,
            Material = dto.Material
        };

        var updated = await itemRepository.UpdateAsync(item);
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
    public async Task<bool> DeleteAsync(int id)
    {
        return await itemRepository.DeleteAsync(id);
    }

    private static ItemDto MapToDto(Item item) => new(
        item.Id, item.Name,
        new ItemCategoryDto(item.Category.Id, item.Category.Name, item.Category.RecommendedCleaning, item.Category.Price),
        item.CategoryId, item.Material);
}