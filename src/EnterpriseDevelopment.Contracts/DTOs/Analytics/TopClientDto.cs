namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Данные клиента с общей суммой оформленных заказов
/// </summary>
public record TopClientDto(
    int ClientId,
    string FullName,
    decimal TotalSpent);
