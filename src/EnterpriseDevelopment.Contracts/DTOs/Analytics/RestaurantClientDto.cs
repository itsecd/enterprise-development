namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Данные клиента ресторана
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="FullName">Полное имя клиента</param>
/// <param name="PhoneNumber">Номер телефона клиента</param>
public record RestaurantClientDto(
    int ClientId,
    string FullName,
    string PhoneNumber);
