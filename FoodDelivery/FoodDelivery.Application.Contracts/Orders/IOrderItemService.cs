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
    IReadOnlyList<OrderItemDto> GetAll(int orderId);

    /// <summary>
    /// Возвращает позицию заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <returns>Позиция заказа</returns>
    OrderItemDto GetById(int orderId, int itemId);

    /// <summary>
    /// Добавляет блюдо в заказ
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="request">Данные позиции</param>
    /// <returns>Созданная позиция</returns>
    OrderItemDto Create(int orderId, AddOrderItemRequest request);

    /// <summary>
    /// Изменяет количество в позиции
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <param name="request">Новое количество</param>
    /// <returns>Изменённая позиция</returns>
    OrderItemDto Update(int orderId, int itemId, UpdateOrderItemRequest request);

    /// <summary>
    /// Удаляет позицию, если она существует
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    void Delete(int orderId, int itemId);
}
