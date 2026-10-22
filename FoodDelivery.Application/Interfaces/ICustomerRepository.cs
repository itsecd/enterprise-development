using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

/// <summary>
/// Определяет операции доступа к данным покупателей
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
}
