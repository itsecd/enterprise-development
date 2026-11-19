using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с заказами.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Получить список всех заказов.
    /// </summary>
    public Task<IEnumerable<OrderDto>> GetAllAsync();

    /// <summary>
    /// Получить заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    public Task<OrderDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новый заказ.
    /// </summary>
    /// <param name="orderDto">Данные нового заказа.</param>
    public Task<OrderDto> CreateAsync(OrderDto orderDto);

    /// <summary>
    /// Обновить данные заказа.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="orderDto">Новые данные заказа.</param>
    public Task UpdateAsync(Guid id, OrderUpdateDto orderDto);

    /// <summary>
    /// Удалить заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    public Task DeleteAsync(Guid id);
}
