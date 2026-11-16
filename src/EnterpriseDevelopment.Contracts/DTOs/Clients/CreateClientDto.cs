namespace EnterpriseDevelopment.Contracts.DTOs.Clients;

/// <summary>
/// Данные для создания клиента
/// </summary>
public record CreateClientDto(
    string FullName,
    string PhoneNumber,
    string DeliveryAddress);
