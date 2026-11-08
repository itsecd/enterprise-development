using DryCleaner.Application.Contracts.Client;

namespace DryCleaner.Application.Contracts.Analytic;

/// <summary>
/// DTO для запроса самых активных клиентов
/// </summary>
/// <param name="Client">Клиент</param>
/// <param name="OrdersCount">Количество заказов клиента</param>
public record ClientOrdersCountDto(ClientDto Client, int OrdersCount);