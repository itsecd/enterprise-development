namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Статистика заказанных блюд определённой категории
/// </summary>
public record CategoryStatisticsDto(
    int CategoryId,
    string CategoryName,
    int OrderCount);
