namespace FoodDelivery.Application.Contracts.Analytics;

/// <summary>
/// Статистика заказов категории за период
/// </summary>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="CategoryName">Название категории</param>
/// <param name="OrderCount">Количество заказов</param>
/// <param name="AverageAmount">Средняя сумма заказа</param>
/// <param name="TotalAmount">Общая сумма заказов</param>
public sealed record CategoryOrderStatisticsDto(
    int CategoryId,
    string CategoryName,
    int OrderCount,
    decimal AverageAmount,
    decimal TotalAmount);
