using EnterpriseDevelopment.Contracts.DTOs.Dishes;

namespace EnterpriseDevelopment.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с блюдами
/// </summary>
public interface IDishService
    : ICrudService<DishDto, CreateDishDto, UpdateDishDto>
{
}
