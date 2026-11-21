using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

/// <summary>
/// Прикладной сервис для работы с категориями блюд
/// </summary>
public interface ICategoryService
    : IApplicationService<CategoryDto, CategoryCreateUpdateDto, int>
{
}