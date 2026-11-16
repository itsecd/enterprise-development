using DryCleaning.Domain.Models;

namespace DryCleaning.Api.Dtos;

/// <summary>
/// Клиент получает
/// </summary>
public record OrderDto(int Id, int ClientId, string ClientName, int ItemId, string ItemName, DateTime AcceptDate, int ExeDays, Status Status);

/// <summary>
/// Клиент присылает при создании
/// </summary>
public record CreateOrderDto(int ClientId, int ItemId, DateTime AcceptDate, int ExeDays, Status Status);

/// <summary>
/// Клиент присылает при обновлении
/// </summary>
public record UpdateOrderDto(int ClientId, int ItemId, DateTime AcceptDate, int ExeDays, Status Status);
