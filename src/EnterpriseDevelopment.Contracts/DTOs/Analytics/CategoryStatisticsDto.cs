namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Статистика заказанных блюд определённой категории
/// </summary>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="CategoryName">Название категории</param>
/// <param name="OrderCount">Количество заказанных блюд</param>
public record CategoryStatisticsDto(
    int CategoryId,
    string CategoryName,
    int OrderCount);
