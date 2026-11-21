namespace TransportCompany.Domain.Models;

/// <summary>
/// Результат аналитики по сумме затрат клиента
/// </summary>
public sealed record ClientTripCost(int ClientId, string ClientName, decimal TotalCost);