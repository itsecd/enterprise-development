using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;


namespace DryCleaner.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для управления данными категорий изделий в оперативной памяти.
/// </summary>
public class ProductCategoryInMemoryRepository : IRepository<ProductCategory, int>
{
    /// <summary>
    /// Список категорий продуктов.
    /// </summary>
    private readonly List<ProductCategory> _productCategories = DataSeed.Categories;

    /// <summary>
    /// Последний сгенерированный идентификатор.
    /// </summary>
    private int _lastId = DataSeed.Categories.Count > 0
    ? DataSeed.Categories.Max(category => category.Id)
    : 0;

    /// <summary>
    /// Добавляет категорию изделия в коллекцию и возвращает добавленный объект.
    /// </summary>
    /// <param name="entity">Категория изделия для добавления.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с добавленной категорией изделия в качестве результата.</returns>
    public Task<ProductCategory> Create(ProductCategory entity)
    {
        entity.Id = ++_lastId;
        _productCategories.Add(entity);
        return Task.FromResult(entity);
    }


    /// <summary>
    /// Удаляет категорию изделия по идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор удаляемой категории изделия.</param>
    /// <returns>Значение true, если категория изделия успешно удалена; в противном случае — false.</returns>
    public Task<bool> Delete(int entityId)
    {
        var productCategory = Read(entityId).Result;
        if (productCategory == null)
            return Task.FromResult(false);

        var result = _productCategories.Remove(productCategory);
        return Task.FromResult(result);

    }

    /// <summary>
    /// Возвращает категорию изделия с указанным идентификатором или null, если категория изделия не найдена.
    /// </summary>
    /// <param name="entityId">Идентификатор категорий изделия.</param>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является объект ProductCategory или null.</returns>
    public Task<ProductCategory?> Read(int entityId) => Task.FromResult(_productCategories.FirstOrDefault(p => p.Id == entityId));

    /// <summary>
    /// Возвращает список всех категорий изделий.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является cписок категорий изделий.</returns>
    public Task<IList<ProductCategory>> ReadAll() => Task.FromResult<IList<ProductCategory>>([.. _productCategories]);

    /// <summary>
    /// Обновляет данные категории изделия или создает новую, если категория изделия не найдена.
    /// </summary>
    /// <param name="entity">Категория изделия с обновленными данными.</param>
    /// <returns>Задача, представляющая обновленную или созданную категорию изделия.</returns>
    public Task<ProductCategory> Update(ProductCategory entity)
    {
        var productCategory = Read(entity.Id).Result;

        if (productCategory == null)
        {
            return Create(entity);
        }

        productCategory.Name = entity.Name;
        productCategory.RecommendedCleaningType = entity.RecommendedCleaningType;
        productCategory.CleaningPrice = entity.CleaningPrice;
        return Task.FromResult(productCategory);
    }
}
