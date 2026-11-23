using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Application.Contracts.DTOs.ItemCategory;
using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Order;
using DryCleaner.Application.Contracts.Interfaces;
using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Services;

/// <summary>
/// Прикладные операции над категориями изделий
/// </summary>
public class ItemCategoryService(IRepository<ItemCategory> categoryRepository) : IItemCategoryService
{
    /// <inheritdoc />
    public async Task<ItemCategoryDto> CreateAsync(ItemCategoryCreateUpdateDto dto)
    {
        var category = new ItemCategory
        {
            Id = 0,
            Name = dto.Name,
            RecommendedCleaning = dto.RecommendedCleaning,
            Price = dto.Price
        };

        var created = await categoryRepository.AddAsync(category);

        return new ItemCategoryDto(
            created.Id,
            created.Name,
            created.RecommendedCleaning,
            created.Price);
    }

    /// <inheritdoc />
    public async Task<ItemCategoryDto?> GetAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null) return null;

        return new ItemCategoryDto(
            category.Id,
            category.Name,
            category.RecommendedCleaning,
            category.Price);
    }

    /// <inheritdoc />
    public async Task<IList<ItemCategoryDto>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(c => new ItemCategoryDto(c.Id, c.Name, c.RecommendedCleaning, c.Price)).ToList();
    }

    /// <inheritdoc />
    public async Task<ItemCategoryDto?> UpdateAsync(ItemCategoryCreateUpdateDto dto, int id)
    {
        var category = new ItemCategory
        {
            Id = id,
            Name = dto.Name,
            RecommendedCleaning = dto.RecommendedCleaning,
            Price = dto.Price
        };

        var updated = await categoryRepository.UpdateAsync(category);
        if (updated is null)
        {
            throw new KeyNotFoundException($"Категория с идентификатором {id} не найдена");
        }

        return new ItemCategoryDto(
            updated.Id,
            updated.Name,
            updated.RecommendedCleaning,
            updated.Price);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await categoryRepository.DeleteAsync(id);
    }
}