using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над категориями
/// </summary>
public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    /// <inheritdoc />
    public async Task<CategoryDto> CreateAsync(CategoryCreateUpdateDto dto)
    {
        var category = new Category
        {
            Id = 0,
            Name = dto.Name
        };

        var createdCategory = await categoryRepository.AddAsync(category);

        return MapToDto(createdCategory);
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> GetAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        return category is null ? null : MapToDto(category);
    }

    /// <inheritdoc />
    public async Task<IList<CategoryDto>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync();

        return categories
            .Select(MapToDto)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<CategoryDto> UpdateAsync(
        CategoryCreateUpdateDto dto,
        int id)
    {
        var category = new Category
        {
            Id = id,
            Name = dto.Name
        };

        var updatedCategory =
            await categoryRepository.UpdateAsync(category);

        if (updatedCategory is null)
        {
            throw new KeyNotFoundException(
                $"Категория с идентификатором {id} не найдена");
        }

        return MapToDto(updatedCategory);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await categoryRepository.DeleteAsync(id);
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto(category.Id, category.Name);
    }
}