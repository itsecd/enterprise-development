using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

public interface ICategoryService
    : IApplicationService<CategoryDto, CategoryCreateUpdateDto, int>
{
}