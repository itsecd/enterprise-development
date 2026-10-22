using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

/// <summary>
/// Определяет операции доступа к данным ресторанов
/// </summary>
public interface IRestaurantRepository : IRepository<Restaurant>
{
}
