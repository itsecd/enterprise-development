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
    public ItemCategoryDto Create(ItemCategoryCreateUpdateDto dto)
    {
        var category = new ItemCategory
        {
            Id = 0,
            Name = dto.Name,
            RecommendedCleaning = dto.RecommendedCleaning,
            Price = dto.Price
        };

        var created = categoryRepository.Add(category);

        return new ItemCategoryDto(
            created.Id,
            created.Name,
            created.RecommendedCleaning,
            created.Price);
    }

    /// <inheritdoc />
    public ItemCategoryDto? Get(int id)
    {
        var category = categoryRepository.GetById(id);
        if (category is null) return null;

        return new ItemCategoryDto(
            category.Id,
            category.Name,
            category.RecommendedCleaning,
            category.Price);
    }

    /// <inheritdoc />
    public IList<ItemCategoryDto> GetAll()
    {
        return categoryRepository.GetAll()
            .Select(c => new ItemCategoryDto(
                c.Id,
                c.Name,
                c.RecommendedCleaning,
                c.Price))
            .ToList();
    }

    /// <inheritdoc />
    public ItemCategoryDto Update(ItemCategoryCreateUpdateDto dto, int id)
    {
        var category = new ItemCategory
        {
            Id = id,
            Name = dto.Name,
            RecommendedCleaning = dto.RecommendedCleaning,
            Price = dto.Price
        };

        var updated = categoryRepository.Update(category);
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
    public bool Delete(int id)
    {
        return categoryRepository.Delete(id);
    }
}