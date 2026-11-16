namespace DryCleaning.Api.Dtos;

/// <summary>
/// Топ клиентов
/// </summary>
public record TopClientDto(int ClientId, string FullName, int ItemsCount);

/// <summary>
/// Популярные категории
/// </summary>
public record CategoryPopularityDto(string CategoryName, int OrdersCount);

/// <summary>
/// Топ затрат
/// </summary>
public record TopSpendingClientDto(int ClientId, string FullName, decimal TotalSpent);
