using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

public interface IDishService
    : IApplicationService<DishDto, DishCreateUpdateDto, int>
{
}