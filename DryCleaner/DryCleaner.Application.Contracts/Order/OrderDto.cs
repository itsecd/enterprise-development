using DryCleaner.Application.Contracts.Client;
using DryCleaner.Application.Contracts.Product;
using DryCleaner.Shared.Enums;


namespace DryCleaner.Application.Contracts.Order;

/// <summary>
/// /// DTO для Get запросов к заказм
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Client">Клиент</param>
/// <param name="Product">Изделие</param>
/// <param name="AcceptedAt">Время приема заказа</param>
/// <param name="ProcessingDays">Срок выполнения в днях</param>
/// <param name="Status">Статус заказа</param>
public record OrderDto(int Id, ClientDto Client, ProductDto Product, DateTime AcceptedAt, int ProcessingDays, OrderStatus Status);
