using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над категориями
/// </summary>
public class CategoryService(
    ICategoryRepository categoryRepository)
    : ICategoryService
{
    /// <inheritdoc />
    public CategoryDto Create(CategoryCreateUpdateDto dto)
    {
        var category = new Category
        {
            Id = 0,
            Name = dto.Name
        };

        var createdCategory = categoryRepository.Add(category);

        return MapToDto(createdCategory);
    }

    /// <inheritdoc />
    public CategoryDto? Get(int id)
    {
        var category = categoryRepository.GetById(id);

        return category is null ? null : MapToDto(category);
    }

    /// <inheritdoc />
    public IList<CategoryDto> GetAll()
    {
        return categoryRepository.GetAll()
            .Select(MapToDto)
            .ToList();
    }

    /// <inheritdoc />
    public CategoryDto Update(CategoryCreateUpdateDto dto, int id)
    {
        var category = new Category
        {
            Id = id,
            Name = dto.Name
        };

        var updatedCategory = categoryRepository.Update(category);

        if (updatedCategory is null)
        {
            throw new KeyNotFoundException(
                $"Категория с идентификатором {id} не найдена");
        }

        return MapToDto(updatedCategory);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        return categoryRepository.Delete(id);
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto(category.Id, category.Name);
    }
}
