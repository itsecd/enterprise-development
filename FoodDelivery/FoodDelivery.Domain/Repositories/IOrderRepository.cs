using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет операции хранения заказов и их позиций
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// Возвращает заказы клиента
    /// </summary>
    /// <param name="customerId">Идентификатор клиента</param>
    /// <returns>Заказы клиента</returns>
    IReadOnlyList<Order> GetByCustomer(int customerId);

    /// <summary>
    /// Возвращает заказы ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Заказы ресторана</returns>
    IReadOnlyList<Order> GetByRestaurant(int restaurantId);

    /// <summary>
    /// Возвращает позиции заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <returns>Позиции или null, если заказ отсутствует</returns>
    IReadOnlyList<OrderItem>? GetItems(int orderId);

    /// <summary>
    /// Возвращает позицию заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <returns>Позиция или null, если она отсутствует</returns>
    OrderItem? GetItem(int orderId, int itemId);

    /// <summary>
    /// Добавляет позицию и назначает её идентификатор
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="item">Данные позиции</param>
    /// <returns>Добавленная позиция или null, если заказ отсутствует</returns>
    OrderItem? AddItem(int orderId, OrderItem item);

    /// <summary>
    /// Изменяет количество в позиции
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <param name="quantity">Новое количество</param>
    /// <returns>Изменённая позиция или null, если она отсутствует</returns>
    OrderItem? UpdateItem(int orderId, int itemId, int quantity);

    /// <summary>
    /// Удаляет позицию, если она существует
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    void DeleteItem(int orderId, int itemId);
}
