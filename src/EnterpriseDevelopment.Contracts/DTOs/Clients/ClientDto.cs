namespace EnterpriseDevelopment.Contracts.DTOs.Clients;

/// <summary>
/// Данные клиента
/// </summary>
/// <param name="Id">Идентификатор клиента</param>
/// <param name="FullName">Полное имя клиента</param>
/// <param name="PhoneNumber">Номер телефона клиента</param>
/// <param name="DeliveryAddress">Адрес доставки клиента</param>
public record ClientDto(
    int Id,
    string FullName,
    string PhoneNumber,
    string DeliveryAddress);
