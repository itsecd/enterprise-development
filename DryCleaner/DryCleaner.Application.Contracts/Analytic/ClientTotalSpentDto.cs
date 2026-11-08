using DryCleaner.Application.Contracts.Client;

namespace DryCleaner.Application.Contracts.Analytic;

/// <summary>
/// DTO для запроса клиента, потратившего самую большую сумму
/// </summary>
/// <param name="Client">Клиент</param>
/// <param name="TotalSpent">Потраченная сумма</param>
public record ClientTotalSpentDto(ClientDto Client, decimal TotalSpent);