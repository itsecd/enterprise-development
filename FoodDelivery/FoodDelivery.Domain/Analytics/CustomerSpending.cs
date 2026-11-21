using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Analytics;

/// <summary>
/// Общая сумма расходов клиента на заказы
/// </summary>
/// <param name="Customer">Клиент</param>
/// <param name="TotalAmount">Общая сумма заказов</param>
public sealed record CustomerSpending(
    Customer Customer,
    decimal TotalAmount);
