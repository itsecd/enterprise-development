namespace FoodDelivery.Application.Contracts.DTOs.Analytics;

/// <summary>
/// Содержит информацию о времени доставки заказа
/// </summary>
/// <param name="OrderId">Идентификатор заказа</param>
/// <param name="DeliveryTime">Продолжительность доставки заказа</param>
public record OrderDeliveryTimeDto(
    int OrderId,
    TimeSpan DeliveryTime);