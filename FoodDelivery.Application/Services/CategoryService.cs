using Microsoft.Extensions.Logging;
using FoodDelivery.Contracts.Categories;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Сервис для управления категориями блюд.
/// </summary>
public class CategoryService(IRepository<DishCategory> repository, ILogger<CategoryService> logger) : ICategoryService
{
    public IReadOnlyList<CategoryDto> GetAll() =>
        [.. repository.GetAll().Select(c => new CategoryDto(c.Id, c.Name))];

    public CategoryDto? GetById(int id)
    {
        var c = repository.GetById(id);
        return c is null ? null : new CategoryDto(c.Id, c.Name);
    }

    public CategoryDto Create(CategoryCreateUpdateDto dto)
    {
        var entity = new DishCategory
        {
            Name = dto.Name
        };
        var created = repository.Create(entity);
        logger.LogInformation("Создана категория с Id = {Id}", created.Id);
        return new CategoryDto(created.Id, created.Name);
    }

    public bool Update(int id, CategoryCreateUpdateDto dto)
    {
        var existing = repository.GetById(id);
        if (existing is null) return false;

        existing.Name = dto.Name;
        var result = repository.Update(existing);
        logger.LogInformation("Обновлена категория с Id = {Id}", id);
        return result;
    }

    public void Delete(int id)
    {
        repository.Delete(id);
        logger.LogInformation("Удалена категория с Id = {Id}", id);
    }
}