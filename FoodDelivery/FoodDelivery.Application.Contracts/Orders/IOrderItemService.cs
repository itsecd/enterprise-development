namespace FoodDelivery.Application.Contracts.Orders;

/// <summary>
/// Определяет операции с позициями заказа
/// </summary>
public interface IOrderItemService
{
    /// <summary>
    /// Возвращает позиции заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <returns>Позиции заказа</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<OrderItemDto>> GetAllAsync(int orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает позицию заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <returns>Позиция заказа</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<OrderItemDto> GetByIdAsync(int orderId, int itemId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет блюдо в заказ
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="request">Данные позиции</param>
    /// <returns>Созданная позиция</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<OrderItemDto> CreateAsync(int orderId, AddOrderItemRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Изменяет количество в позиции
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <param name="request">Новое количество</param>
    /// <returns>Изменённая позиция</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<OrderItemDto> UpdateAsync(int orderId, int itemId, UpdateOrderItemRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет позицию, если она существует
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task DeleteAsync(int orderId, int itemId, CancellationToken cancellationToken = default);
}
