using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

/// <summary>
/// Определяет операции доступа к данным блюд
/// </summary>
public interface IDishRepository : IRepository<Dish>
{
}
