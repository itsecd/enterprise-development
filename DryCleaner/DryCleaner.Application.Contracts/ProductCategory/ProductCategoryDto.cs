namespace DryCleaner.Application.Contracts.ProductCategory;

/// <summary>
/// DTO для Get запросов к категориям изделий
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название категории</param>
/// <param name="RecommendedCleaningType">Рекомендуемый тип чистки</param>
/// <param name="CleaningPrice">Цена чистки</param>
public record ProductCategoryDto(int Id, string Name, string? RecommendedCleaningType, decimal CleaningPrice);

