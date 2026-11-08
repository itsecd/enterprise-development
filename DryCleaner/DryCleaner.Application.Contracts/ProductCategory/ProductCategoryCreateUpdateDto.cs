namespace DryCleaner.Application.Contracts.ProductCategory;

/// <summary>
/// DTO для Post, Put запросов к категориям изделий
/// </summary>
/// <param name="Name">Название категории</param>
/// <param name="RecommendedCleaningType">Рекомендуемый тип чистки</param>
/// <param name="CleaningPrice">Цена чистки</param>
public record ProductCategoryCreateUpdateDto(string Name, string? RecommendedCleaningType, decimal CleaningPrice);

