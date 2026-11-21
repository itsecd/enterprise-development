using DryCleaner.Application.Contracts.Client;

namespace DryCleaner.Application.Contracts.Analytic;

/// <summary>
/// DTO для запроса среднего срока выполнения заказов клиента
/// </summary>
/// <param name="Client">Клиент</param>
/// <param name="AverageProcessingDays">Средний срок выполнения заказов в днях</param>
public record ClientAverageProcessingDaysDto(ClientDto Client, double AverageProcessingDays);