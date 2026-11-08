namespace DryCleaner.Application.Contracts.Analytic;

/// <summary>
/// DTO для запроса самых и наименее популярных категорий изделий
/// </summary>
/// <param name="Category">Топ 5 самых популярных категорий</param>
public record CategoriesPopularityDto(IList<CategoryOrdersCountDto> Category);