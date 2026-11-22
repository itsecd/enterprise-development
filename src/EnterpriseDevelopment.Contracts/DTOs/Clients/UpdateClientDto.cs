namespace EnterpriseDevelopment.Contracts.DTOs.Clients;

/// <summary>
/// Данные для обновления клиента
/// </summary>
/// <param name="FullName">Новое полное имя клиента</param>
/// <param name="PhoneNumber">Новый номер телефона клиента</param>
/// <param name="DeliveryAddress">Новый адрес доставки клиента</param>
public record UpdateClientDto(
    string FullName,
    string PhoneNumber,
    string DeliveryAddress);
