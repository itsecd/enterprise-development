using AutoService.Domain.Models;

namespace AutoService.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с заказами.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Получить список всех заказов.
    /// </summary>
    public Task<IEnumerable<Order>> GetAllAsync();

    /// <summary>
    /// Получить заказ по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    public Task<Order?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новый заказ.
    /// </summary>
    /// <param name="order">Объект заказа.</param>
    public Task AddAsync(Order order);

    /// <summary>
    /// Обновить данные существующего заказа.
    /// </summary>
    /// <param name="order">Объект заказа с обновленными данными.</param>
    public Task UpdateAsync(Order order);

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    /// <param name="order">Объект заказа для удаления.</param>
    public Task DeleteAsync(Order order);
}