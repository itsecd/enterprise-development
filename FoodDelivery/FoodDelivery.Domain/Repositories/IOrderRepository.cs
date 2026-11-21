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
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<Order>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает заказы ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Заказы ресторана</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<Order>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает позиции заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <returns>Позиции или null, если заказ отсутствует</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<OrderItem>?> GetItemsAsync(int orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает позицию заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <returns>Позиция или null, если она отсутствует</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<OrderItem?> GetItemAsync(int orderId, int itemId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет позицию и назначает её идентификатор
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="item">Данные позиции</param>
    /// <returns>Добавленная позиция или null, если заказ отсутствует</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<OrderItem?> AddItemAsync(int orderId, OrderItem item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Изменяет количество в позиции
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <param name="quantity">Новое количество</param>
    /// <returns>Изменённая позиция или null, если она отсутствует</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<OrderItem?> UpdateItemAsync(int orderId, int itemId, int quantity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет позицию, если она существует
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task DeleteItemAsync(int orderId, int itemId, CancellationToken cancellationToken = default);
}
