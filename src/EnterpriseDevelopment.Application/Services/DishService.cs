using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Contracts.DTOs.Dishes;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Application.Services;

/// <summary>
/// Сервис для управления блюдами
/// </summary>
public class DishService(
    IDishRepository repository,
    ICategoryRepository categoryRepository,
    IRestaurantRepository restaurantRepository,
    IOrderRepository orderRepository) : IDishService
{
    /// <summary>
    /// Получает все блюда
    /// </summary>
    /// <returns>Коллекция блюд</returns>
    public IEnumerable<DishDto> GetAll()
    {
        return repository.GetAll().Select(ToDto);
    }

    /// <summary>
    /// Получает блюдо по идентификатору
    /// </summary>
    /// <returns>Данные блюда или null</returns>
    public DishDto? GetById(int id)
    {
        var dish = repository.GetById(id);

        return dish is null ? null : ToDto(dish);
    }

    /// <summary>
    /// Создаёт новое блюдо
    /// </summary>
    /// <returns>Данные созданного блюда</returns>
    public DishDto Create(CreateDishDto dto)
    {
        Validate(
            dto.Name,
            dto.Weight,
            dto.Price,
            dto.CategoryId,
            dto.RestaurantId);

        var dish = new Dish
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            RestaurantId = dto.RestaurantId,
            Category = categoryRepository.GetById(dto.CategoryId)!,
            Restaurant = restaurantRepository.GetById(dto.RestaurantId)!
        };

        return ToDto(repository.Add(dish));
    }

    /// <summary>
    /// Обновляет существующее блюдо
    /// </summary>
    /// <returns>true, если блюдо обновлено, иначе false</returns>
    public bool Update(int id, UpdateDishDto dto)
    {
        var dish = repository.GetById(id);

        if (dish is null)
        {
            return false;
        }

        Validate(
            dto.Name,
            dto.Weight,
            dto.Price,
            dto.CategoryId,
            dto.RestaurantId);

        dish.Name = dto.Name;
        dish.Weight = dto.Weight;
        dish.Price = dto.Price;
        dish.CategoryId = dto.CategoryId;
        dish.RestaurantId = dto.RestaurantId;
        dish.Category = categoryRepository.GetById(dto.CategoryId)!;
        dish.Restaurant = restaurantRepository.GetById(dto.RestaurantId)!;

        repository.Update(dish);

        return true;
    }

    /// <summary>
    /// Удаляет блюдо
    /// </summary>
    /// <returns>true, если блюдо существовало, иначе false</returns>
    public bool Delete(int id)
    {
        var dish = repository.GetById(id);

        if (dish is null)
        {
            return false;
        }

        if (orderRepository.GetAll().Any(order =>
            order.Dishes.Any(orderedDish => orderedDish.Id == id)))
        {
            throw new InvalidOperationException(
                "Нельзя удалить блюдо, которое входит в заказ");
        }

        repository.Delete(dish);

        return true;
    }

    private static DishDto ToDto(Dish dish)
    {
        return new DishDto(
            dish.Id,
            dish.Name,
            dish.Weight,
            dish.Price,
            dish.CategoryId,
            dish.RestaurantId);
    }
    private void Validate(
        string name,
        int weight,
        decimal price,
        int categoryId,
        int restaurantId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название блюда не может быть пустым");
        }

        if (weight <= 0)
        {
            throw new ArgumentException("Вес блюда должен быть положительным");
        }

        if (price <= 0)
        {
            throw new ArgumentException("Цена блюда должна быть положительной");
        }

        if (categoryRepository.GetById(categoryId) is null)
        {
            throw new KeyNotFoundException("Категория блюда не найдена");
        }

        if (restaurantRepository.GetById(restaurantId) is null)
        {
            throw new KeyNotFoundException("Ресторан блюда не найден");
        }
    }
}
