using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Analytics;

/// <summary>
/// Статистика заказов категории за период
/// </summary>
/// <param name="Category">Категория блюд</param>
/// <param name="OrderCount">Количество заказов</param>
/// <param name="AverageAmount">Средняя сумма заказа</param>
/// <param name="TotalAmount">Общая сумма заказов</param>
public sealed record CategoryOrderStatistics(
    Category Category,
    int OrderCount,
    decimal AverageAmount,
    decimal TotalAmount);
