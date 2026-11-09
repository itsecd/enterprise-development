namespace EnterpriseDevelopment.Contracts.DTOs.Clients;

/// <summary>
/// Данные для обновления клиента
/// </summary>
public record UpdateClientDto(
    string FullName,
    string PhoneNumber,
    string DeliveryAddress);
