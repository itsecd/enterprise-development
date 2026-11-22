namespace EnterpriseDevelopment.Contracts.DTOs.Clients;

/// <summary>
/// Данные для создания клиента
/// </summary>
/// <param name="FullName">Полное имя создаваемого клиента</param>
/// <param name="PhoneNumber">Номер телефона клиента</param>
/// <param name="DeliveryAddress">Адрес доставки клиента</param>
public record CreateClientDto(
    string FullName,
    string PhoneNumber,
    string DeliveryAddress);
