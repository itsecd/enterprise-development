using System.ComponentModel.DataAnnotations;
using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Exceptions;
using DryCleaning.Contracts.Interfaces;
using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;

namespace DryCleaning.Application.Services;

public class ItemService(
    IItemRepository itemRepository,
    IItemCategoryRepository categoryRepository,
    IOrderRepository orderRepository) : IItemService
{
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly IItemCategoryRepository _categoryRepository = categoryRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<IReadOnlyList<ItemDto>> GetAllAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        return [.. items.Select(MapToDto)];
    }

    public async Task<IReadOnlyList<ItemDto>?> GetByCategoryIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null) return null;
        var items = await _itemRepository.GetByCategoryIdAsync(categoryId);
        return [.. items.Select(MapToDto)];
    }

    public async Task<ItemDto?> GetByIdAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<ItemDto> AddAsync(ItemEditDto item)
    {
        Validate(item);
        var category = await _categoryRepository.GetByIdAsync(item.CategoryId) ?? throw new ValidationException("Категория не найдена");
        var created = await _itemRepository.AddAsync(new Item { Name = item.Name, Material = item.Material, Category = category });
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, ItemEditDto item)
    {
        Validate(item);
        var existing = await _itemRepository.GetByIdAsync(id);
        if (existing == null) return false;
        var category = await _categoryRepository.GetByIdAsync(item.CategoryId) ?? throw new ValidationException("Категория не найдена");
        var updated = new Item { ItemId = id, Name = item.Name, Material = item.Material, Category = category };
        return await _itemRepository.UpdateAsync(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item is null) return false;
        if (await _orderRepository.HasOrdersForItemAsync(id))
            throw new ItemHasOrdersException(id);
        return await _itemRepository.DeleteAsync(id);
    }

    private static ItemDto MapToDto(Item item) => new() { ItemId = item.ItemId, Name = item.Name, Material = item.Material, Category = new ItemCategoryDto { CategoryId = item.Category.CategoryId, CategoryName = item.Category.CategoryName, RecommendedCleaningType = item.Category.RecommendedCleaningType, Price = item.Category.Price } };

    private static void Validate(object request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);
    }
}
