using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Contracts.DTOs.Categories;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Application.Services;

/// <summary>
/// Сервис для управления категориями блюд
/// </summary>
public class CategoryService(
    ICategoryRepository repository,
    IDishRepository dishRepository) : ICategoryService
{
    /// <summary>
    /// Получает все категории блюд
    /// </summary>
    /// <returns>Коллекция категорий блюд</returns>
    public IEnumerable<CategoryDto> GetAll()
    {
        return repository.GetAll().Select(ToDto);
    }

    /// <summary>
    /// Получает категорию по идентификатору
    /// </summary>
    /// <returns>Данные категории или null</returns>
    public CategoryDto? GetById(int id)
    {
        var category = repository.GetById(id);

        return category is null ? null : ToDto(category);
    }

    /// <summary>
    /// Создаёт новую категорию блюд
    /// </summary>
    /// <returns>Данные созданной категории</returns>
    public CategoryDto Create(CreateCategoryDto dto)
    {
        Validate(dto.Name);

        var category = new DishCategory
        {
            Name = dto.Name
        };

        return ToDto(repository.Add(category));
    }

    /// <summary>
    /// Обновляет существующую категорию
    /// </summary>
    /// <returns>true, если категория обновлена, иначе false</returns>
    public bool Update(int id, UpdateCategoryDto dto)
    {
        var category = repository.GetById(id);

        if (category is null)
        {
            return false;
        }

        Validate(dto.Name);

        category.Name = dto.Name;

        repository.Update(category);

        return true;
    }

    /// <summary>
    /// Удаляет категорию блюд
    /// </summary>
    /// <returns>true, если категория существовала, иначе false</returns>
    public bool Delete(int id)
    {
        var category = repository.GetById(id);

        if (category is null)
        {
            return false;
        }

        if (dishRepository.GetAll().Any(dish => dish.CategoryId == id))
        {
            throw new InvalidOperationException(
                "Нельзя удалить категорию, к которой привязаны блюда");
        }

        repository.Delete(category);

        return true;
    }

    private static CategoryDto ToDto(DishCategory category)
    {
        return new CategoryDto(
            category.Id,
            category.Name);
    }

    private static void Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Название категории не может быть пустым");
        }
    }
}
