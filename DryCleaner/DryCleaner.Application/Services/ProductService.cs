using DryCleaner.Application.Contracts.Product;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Mappers;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для управления изделиями и их категориями.
/// </summary>
/// <param name="productRepository">Репозиторий для работы с изделиями.</param>
/// <param name="productCategoryRepository">Репозиторий для работы с категориями изделий.</param>
public class ProductService(
    IRepository<Product, int> productRepository,
    IRepository<ProductCategory, int> productCategoryRepository)
    : IProductService
{
    /// <summary>
    /// Создает новый продукт на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Данные для создания или обновления продукта.</param>
    /// <returns>DTO созданного продукта.</returns>
    /// <exception cref="KeyNotFoundException">Выбрасывается, если категория продукта с указанным идентификатором не найдена.</exception>
    public async Task<ProductDto> Create(ProductCreateUpdateDto dto)
    {
        var category = await productCategoryRepository.Read(dto.ProductCategoryId);

        if (category == null)
            throw new KeyNotFoundException($"Категория изделия с Id='{dto.ProductCategoryId}' не найдена");

        var product = ProductMapper.Map(dto, category);

        var result = await productRepository.Create(product);

        return ProductMapper.Map(result);
    }

    /// <summary>
    /// Возвращает продукт по идентификатору в виде объекта ProductDto или null, если продукт не найден.
    /// </summary>
    /// <param name="id">Идентификатор продукта.</param>
    /// <returns>Объект ProductDto, представляющий продукт, или null, если продукт не найден.</returns>
    public async Task<ProductDto?> Get(int id)
    {
        var product = await productRepository.Read(id);

        if (product == null)
            return null;

        return ProductMapper.Map(product);
    }

    /// <summary>
    /// Возвращает список всех продуктов в виде объектов ProductDto.
    /// </summary>
    /// <returns>Список объектов ProductDto.</returns>
    public async Task<IList<ProductDto>> GetAll()
    {
        return ProductMapper.Map(await productRepository.ReadAll());
    }

    /// <summary>
    /// Обновляет изделие на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Данные для обновления изделия.</param>
    /// <param name="id">Идентификатор обновляемого изделия.</param>
    /// <returns>Обновлённые данные изделия.</returns>
    /// <exception cref="KeyNotFoundException">Выбрасывается, если категория изделия не найдена.</exception>
    public async Task<ProductDto> Update(ProductCreateUpdateDto dto, int id)
    {
        var category = await productCategoryRepository.Read(dto.ProductCategoryId);

        if (category == null)
            throw new KeyNotFoundException($"Категория изделия с Id='{dto.ProductCategoryId}' не найдена");

        var product = ProductMapper.Map(dto, category);
        product.Id = id;

        var result = await productRepository.Update(product);

        return ProductMapper.Map(result);
    }

    /// <summary>
    /// Удаляет продукт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого продукта.</param>
    /// <returns>Значение true, если продукт успешно удалён; в противном случае — false.</returns>
    public async Task<bool> Delete(int id)
    {
        var result = await productRepository.Delete(id);

        return result;
    }
}
