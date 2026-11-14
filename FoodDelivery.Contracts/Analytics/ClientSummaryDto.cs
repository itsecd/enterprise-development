namespace FoodDelivery.Contracts.Analytics;

/// <summary>
/// Краткая сводная информация о клиенте.
/// </summary>
public record ClientSummaryDto(
    int ClientId,
    string FullName,
    string Phone,
    string DeliveryAddress);