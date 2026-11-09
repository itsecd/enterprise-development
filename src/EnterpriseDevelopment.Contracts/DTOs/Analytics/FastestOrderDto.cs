namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Данные заказа с минимальным временем доставки
/// </summary>
public record FastestOrderDto(
    int OrderId,
    int ClientId,
    int RestaurantId,
    TimeSpan DeliveryTime);
