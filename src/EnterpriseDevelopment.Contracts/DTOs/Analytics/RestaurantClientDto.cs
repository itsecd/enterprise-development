namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Данные клиента ресторана
/// </summary>
public record RestaurantClientDto(
    int ClientId,
    string FullName,
    string PhoneNumber);
