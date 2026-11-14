namespace FoodDelivery.Contracts.Analytics;

/// <summary>
/// Сводная статистика заказов по категории блюд.
/// </summary>
public record CategorySummaryDto(
    int CategoryId,
    string CategoryName,
    int OrderCount,
    decimal AverageOrderPrice,
    decimal TotalOrderPrice);