namespace FoodDelivery.Contracts.Clients;

/// <summary>
/// Информация о клиенте службы доставки.
/// </summary>
public record ClientDto(int Id, string FullName, string Phone, string DeliveryAddress);