using DryCleaner.Shared.Enums;

namespace DryCleaner.Application.Contracts.Order;

/// <summary>
/// DTO для Post, Put запросов к изделиям
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ProductId">Идентификатор изделия</param>
/// <param name="AcceptedAt">Время приема заказа</param>
/// <param name="ProcessingDays">Срок выполнения в днях</param>
/// <param name="Status">Статус заказа</param>
public record OrderCreateUpdateDto(int ClientId, int ProductId, DateTime AcceptedAt, int ProcessingDays, OrderStatus Status);
