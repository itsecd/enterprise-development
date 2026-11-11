using DryCleaner.Application.Contracts.Product;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Mappers;

/// <summary>
/// Преобразует сущности Product в объекты ProductDto или ProductDto в сущности Product.
/// </summary>
internal class ProductMapper
{
    /// <summary>
    /// Преобразует сущность Product в объект ProductDto.
    /// </summary>
    /// <param name="entity">Сущность Product для преобразования.</param>
    /// <returns>Экземпляр ProductDto, созданный на основе переданной сущности.</returns>
    public static ProductDto Map(Product entity)
    {
        return new(entity.Id, entity.Name, entity.Material, ProductCategoryMapper.Map(entity.Category));
    }

    /// <summary>
    /// Преобразует коллекцию сущностей Product в коллекцию объектов ProductDto.
    /// </summary>
    /// <param name="entity">Коллекция сущностей Product для преобразования.</param>
    /// <returns>Коллекция объектов ProductDto.</returns>
    public static IList<ProductDto> Map(IList<Product> entity)
    {
        return [.. entity.Select(Map)];
    }

    /// <summary>
    /// Создает новый экземпляр Product на основе данных из DTO и категории.
    /// </summary>
    /// <param name="dto">Данные для создания или обновления продукта.</param>
    /// <param name="category">Категория продукта.</param>
    /// <returns>Новый экземпляр Product.</returns>
    public static Product Map(ProductCreateUpdateDto dto, ProductCategory category)
    {
        return new Product { Id = 0, Name = dto.Name, Material = dto.Material, Category = category };
    }
}
