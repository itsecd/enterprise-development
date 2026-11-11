using DryCleaner.Application.Contracts.ProductCategory;

namespace DryCleaner.Application.Contracts.Analytic;

/// <summary>
/// DTO для запроса популярности категорий изделий
/// </summary>
/// <param name="Category">Категория изделия</param>
/// <param name="OrdersCount">Количество заказов изделий этой категории</param>
public record CategoryOrdersCountDto(ProductCategoryDto Category, int OrdersCount);