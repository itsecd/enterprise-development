using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

/// <summary>
/// Определяет операции доступа к данным заказов
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
}
