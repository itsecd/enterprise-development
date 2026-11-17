using DryCleaner.Application.Contracts.Product;

namespace DryCleaner.Application.Contracts.ServiceInterfaces;

/// <summary>
/// Определяет контракт для работы с изделиями
/// </summary>
public interface IProductService : IAplicationService<ProductDto, ProductCreateUpdateDto, int>;
