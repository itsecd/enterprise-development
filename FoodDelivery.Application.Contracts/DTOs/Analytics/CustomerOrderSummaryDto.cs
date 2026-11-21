namespace FoodDelivery.Application.Contracts.DTOs.Analytics;

/// <summary>
/// Содержит информацию о клиенте и общей сумме его заказов
/// </summary>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="LastName">Фамилия клиента</param>
/// <param name="FirstName">Имя клиента</param>
/// <param name="Patronymic">Отчество клиента, если оно указано</param>
/// <param name="TotalOrderAmount">Суммарная стоимость заказов клиента</param>
public record CustomerOrderSummaryDto(
    int CustomerId,
    string LastName,
    string FirstName,
    string? Patronymic,
    decimal TotalOrderAmount);