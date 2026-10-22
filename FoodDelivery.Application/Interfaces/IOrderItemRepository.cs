using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

/// <summary>
/// Определяет операции доступа к позициям заказов
/// </summary>
public interface IOrderItemRepository : IRepository<OrderItem>
{
}
