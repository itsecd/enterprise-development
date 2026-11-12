namespace FoodDelivery.Contracts.Clients;

/// <summary>
/// Данные для создания или обновления клиента.
/// </summary>
public record ClientCreateUpdateDto(string FullName, string Phone, string DeliveryAddress);