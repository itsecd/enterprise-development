namespace DryCleaner.Application.Contracts.DTOs.Analytics;

/// <summary>
/// Информация о заказах, находящихся в процессе выполнения
/// </summary>
/// <param name="OrderId">Идентификатор заказа</param>
/// <param name="CustomerName">ФИО клиента</param>
/// <param name="AcceptanceDate">Дата приёма изделия</param>
/// <param name="Status">Статус заказа</param>
public record OrderInProgressDto(
    int OrderId,
    string CustomerName,
    DateOnly AcceptanceDate,
    string Status);