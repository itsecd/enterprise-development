using DryCleaner.Application.Contracts.ProductCategory;

namespace DryCleaner.Application.Contracts.ServiceInterfaces;

/// <summary>
/// Определяет контракт для работы с категориями изделий
/// </summary>
public interface IProductCategoryService : IAplicationService<ProductCategoryDto, ProductCategoryCreateUpdateDto, int>;
