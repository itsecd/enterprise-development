namespace FoodDelivery.Contracts.Analytics;

/// <summary>
/// DTO заказа с минимальной длительностью доставки.
/// </summary>
public record MinDeliveryOrderDto(
    int OrderId,
    double DeliveryDurationMinutes,
    DateTime OrderDateTime,
    DateTime DeliveryDateTime);