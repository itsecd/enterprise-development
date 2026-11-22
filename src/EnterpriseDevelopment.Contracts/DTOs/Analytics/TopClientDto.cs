namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Данные клиента с общей суммой оформленных заказов
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="FullName">Полное имя клиента</param>
/// <param name="TotalSpent">Общая сумма заказов клиента</param>
public record TopClientDto(
    int ClientId,
    string FullName,
    decimal TotalSpent);
