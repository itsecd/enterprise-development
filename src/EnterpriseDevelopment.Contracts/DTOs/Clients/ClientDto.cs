namespace EnterpriseDevelopment.Contracts.DTOs.Clients;

/// <summary>
/// Данные клиента
/// </summary>
public record ClientDto(
    int Id,
    string FullName,
    string PhoneNumber,
    string DeliveryAddress);
