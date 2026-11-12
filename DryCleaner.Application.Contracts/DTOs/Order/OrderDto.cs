using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Shared.Enums;

namespace DryCleaner.Application.Contracts.DTOs.Order;

/// <summary>
/// DTO заказа
/// </summary>
/// <param name="Id">Идентификатор заказа</param>
/// <param name="Customer">Клиент</param>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="Item">Изделие</param>
/// <param name="ItemId">Идентификатор изделия</param>
/// <param name="AcceptanceDate">Дата приема заказа</param>
/// <param name="CompletionDays">Время выполнения заказа</param>
/// <param name="Status">Статус заказа</param>
public record OrderDto(
    int Id,
    CustomerDto Customer,
    int CustomerId,
    ItemDto Item,
    int ItemId,
    DateOnly AcceptanceDate,
    int CompletionDays,
    OrderStatus Status
);