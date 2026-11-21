namespace DryCleaner.Application.Contracts.Product;

/// <summary>
/// DTO для Post, Put запросов к изделиям
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название изделия</param>
/// <param name="Material">Материал изделия</param>
/// <param name="ProductCategoryId">Идентификатор категории изделия</param>
public record ProductCreateUpdateDto(string Name, string Material, int ProductCategoryId);
