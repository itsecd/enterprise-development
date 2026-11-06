using FoodDelivery.Application.Contracts.Customers;

namespace FoodDelivery.Application.Contracts.Analytics;

/// <summary>
/// Клиент с наибольшей суммой заказов
/// </summary>
/// <param name="Customer">Сведения о клиенте</param>
/// <param name="TotalAmount">Общая сумма заказов клиента</param>
public sealed record CustomerSpendingDto(
    CustomerDto Customer,
    decimal TotalAmount);
