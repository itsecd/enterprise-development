using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

/// <summary>
/// Прикладной сервис для работы с блюдами
/// </summary>
public interface IDishService
    : IApplicationService<DishDto, DishCreateUpdateDto, int>
{
}