namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Данные заказа с минимальным временем доставки
/// </summary>
/// <param name="OrderId">Идентификатор заказа</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="RestaurantId">Идентификатор ресторана</param>
/// <param name="DeliveryTime">Время доставки заказа</param>
public record FastestOrderDto(
    int OrderId,
    int ClientId,
    int RestaurantId,
    TimeSpan DeliveryTime);
