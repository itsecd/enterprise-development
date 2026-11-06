using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет операции хранения клиентов
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    /// <summary>
    /// Проверяет наличие заказов клиента
    /// </summary>
    /// <param name="customerId">Идентификатор клиента</param>
    /// <returns>Признак наличия заказов</returns>
    bool HasOrders(int customerId);
}
