namespace DryCleaner.Application.Contracts.DTOs.Analytics;

/// <summary>
/// Информация о количестве заказов клиента
/// </summary>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="CustomerName">ФИО клиента</param>
/// <param name="OrderCount">Количество заказов клиента</param>
public record CustomerOrderCountDto(
    int CustomerId,
    string CustomerName,
    int OrderCount);