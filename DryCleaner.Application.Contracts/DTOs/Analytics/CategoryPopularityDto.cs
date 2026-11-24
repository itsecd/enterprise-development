namespace DryCleaner.Application.Contracts.DTOs.Analytics;

/// <summary>
/// Информация о количестве заказов по категориям изделий
/// </summary>
/// <param name="CategoryId">Идентификатор категории изделия</param>
/// <param name="CategoryName">Название категории изделия</param>
/// <param name="OrderCount">Количество заказов с данной категорией изделия</param>
public record CategoryPopularityDto(
    int CategoryId,
    string CategoryName,
    int OrderCount);