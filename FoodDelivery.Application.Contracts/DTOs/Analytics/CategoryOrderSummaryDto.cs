namespace FoodDelivery.Application.Contracts.DTOs.Analytics;

/// <summary>
/// Содержит сводную информацию о заказах по категории блюд
/// </summary>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="CategoryName">Название категории</param>
/// <param name="OrderCount">Количество заказов, содержащих блюда категории</param>
/// <param name="AverageOrderAmount">Средняя полная стоимость таких заказов</param>
/// <param name="TotalOrderAmount">Суммарная полная стоимость таких заказов</param>
public record CategoryOrderSummaryDto(
    int CategoryId,
    string CategoryName,
    int OrderCount,
    decimal AverageOrderAmount,
    decimal TotalOrderAmount);