using DryCleaner.Domain.Entities;

namespace DryCleaner.Domain.Repositories;

/// <summary>
/// Репозиторий для работы с заказами
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// Получить заказы в обработке
    /// </summary>
    Task<IList<Order>> GetInProgressOrdersAsync();

    /// <summary>
    /// Получить заказы за указанный период
    /// </summary>
    Task<IList<Order>> GetOrdersByPeriodAsync(DateOnly start, DateOnly end);

    /// <summary>
    /// Получить заказы с наибольшим сроком выполнения
    /// </summary>
    Task<IList<Order>> GetOrdersWithMaxCompletionDaysAsync();

    /// <summary>
    /// Получить все заказы с подгруженными клиентами и изделиями
    /// </summary>
    Task<IList<Order>> GetAllWithDetailsAsync();
}