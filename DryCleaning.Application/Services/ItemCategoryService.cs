using System.ComponentModel.DataAnnotations;
using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Exceptions;
using DryCleaning.Contracts.Interfaces;
using DryCleaning.Domain;
using DryCleaning.Domain.Enum;
using DryCleaning.Domain.Interfaces;

namespace DryCleaning.Application.Services;

public class ItemCategoryService(
    IItemCategoryRepository repository,
    IItemRepository itemRepository,
    IOrderRepository orderRepository) : IItemCategoryService
{
    private readonly IItemCategoryRepository _repository = repository;
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<IReadOnlyList<ItemCategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return [.. categories.Select(MapToDto)];
    }

    public async Task<ItemCategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category is null ? null : MapToDto(category);
    }

    public async Task<ItemCategoryDto> AddAsync(ItemCategoryEditDto category)
    {
        Validate(category);
        var newCategory = new ItemCategory { CategoryName = category.CategoryName, RecommendedCleaningType = category.RecommendedCleaningType, Price = category.Price };
        var created = await _repository.AddAsync(newCategory);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, ItemCategoryEditDto category)
    {
        Validate(category);
        var updated = new ItemCategory { CategoryId = id, CategoryName = category.CategoryName, RecommendedCleaningType = category.RecommendedCleaningType, Price = category.Price };
        return await _repository.UpdateAsync(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null) return false;
        if (await _itemRepository.HasItemsForCategoryAsync(id))
            throw new CategoryHasItemsException(id);
        return await _repository.DeleteAsync(id);
    }

    public async Task<CategoryPopularityDto> GetPopularityAsync(DateTime end)
    {
        if (end.Year <= 1) throw new ArgumentOutOfRangeException(nameof(end), "Некорректная дата окончания.");
        var start = end.AddYears(-1);
        var categories = await _repository.GetAllAsync();
        var orders = await _orderRepository.GetAllAsync();

        var periodOrders = orders
            .Where(order => order.AcceptDate >= start && order.AcceptDate < end && order.Status != OrderStatus.Cancelled)
            .ToList();

        var entries = categories
            .Select(category => new CategoryPopularityEntryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ItemCount = periodOrders.Count(order => order.Item.Category.CategoryId == category.CategoryId)
            })
            .ToList();

        return new CategoryPopularityDto
        {
            MostPopular = [.. entries.OrderByDescending(e => e.ItemCount).ThenBy(e => e.CategoryName, StringComparer.Ordinal).ThenBy(e => e.CategoryId).Take(5)],
            LeastPopular = [.. entries.OrderBy(e => e.ItemCount).ThenBy(e => e.CategoryName, StringComparer.Ordinal).ThenBy(e => e.CategoryId).Take(5)]
        };
    }

    private static ItemCategoryDto MapToDto(ItemCategory category) => new() { CategoryId = category.CategoryId, CategoryName = category.CategoryName, RecommendedCleaningType = category.RecommendedCleaningType, Price = category.Price };

    private static void Validate(object request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);
    }
}
