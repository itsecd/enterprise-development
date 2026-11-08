using DryCleaner.Application.Contracts.ProductCategory;

namespace DryCleaner.Application.Contracts.Product;

/// <summary>
/// DTO для Get запросов к изделиям
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название изделия</param>
/// <param name="Material">Материал изделия</param>
/// <param name="Category">Категория изделия</param>
public record ProductDto(int Id, string Name, string Material, ProductCategoryDto Category);

