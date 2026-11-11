using DryCleaner.Application.Contracts.ProductCategory;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Mappers;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;


namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для управления категориями продуктов.
/// </summary>
/// <param name="productCategoryRepository">Репозиторий для работы с категориями продуктов.</param>
public class ProductCategoryService(IRepository<ProductCategory, int> productCategoryRepository) : IProductCategoryService
{
    /// <summary>
    /// Создает новую категорию продукта на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Данные для создания или обновления категории продукта.</param>
    /// <returns>Объект категории продукта, созданный в системе.</returns>
    public async Task<ProductCategoryDto> Create(ProductCategoryCreateUpdateDto dto)
    {
        var productCategory = ProductCategoryMapper.Map(dto);

        var result = await productCategoryRepository.Create(productCategory);

        return ProductCategoryMapper.Map(result);
    }

    /// <summary>
    /// Возвращает DTO категории продукта по идентификатору или null, если категория не найдена.
    /// </summary>
    /// <param name="id">Идентификатор категории продукта.</param>
    /// <returns>DTO категории продукта или null, если категория не найдена.</returns>
    public async Task<ProductCategoryDto?> Get(int id)
    {
        var productCategory = await productCategoryRepository.Read(id);

        if (productCategory == null)
            return null;

        return ProductCategoryMapper.Map(productCategory);
    }

    /// <summary>
    /// Возвращает все категории продуктов в виде списка DTO.
    /// </summary>
    /// <returns>Список объектов ProductCategoryDto, представляющих все категории продуктов.</returns>
    public async Task<IList<ProductCategoryDto>> GetAll()
    {
        return ProductCategoryMapper.Map(await productCategoryRepository.ReadAll());
    }

    /// <summary>
    /// Обновляет категорию продукта на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Данные для обновления категории продукта.</param>
    /// <param name="id">Идентификатор обновляемой категории продукта.</param>
    /// <returns>Обновлённая категория продукта в виде объекта ProductCategoryDto.</returns>
    public async Task<ProductCategoryDto> Update(ProductCategoryCreateUpdateDto dto, int id)
    {
        var productCategory = ProductCategoryMapper.Map(dto);
        productCategory.Id = id;

        var result = await productCategoryRepository.Update(productCategory);

        return ProductCategoryMapper.Map(result);
    }

    /// <summary>
    /// Удаляет категорию продукта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой категории продукта.</param>
    /// <returns>Значение true, если удаление выполнено успешно; в противном случае — false.</returns>
    public async Task<bool> Delete(int id)
    {
        var result = await productCategoryRepository.Delete(id);

        return result;
    }
}
