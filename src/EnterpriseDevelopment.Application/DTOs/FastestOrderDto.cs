namespace EnterpriseDevelopment.Application.DTOs;

/// <summary>
/// Данные заказа с минимальным временем доставки
/// </summary>
public record FastestOrderDto(
    int OrderId,
    int ClientId,
    int RestaurantId,
    TimeSpan DeliveryTime);
