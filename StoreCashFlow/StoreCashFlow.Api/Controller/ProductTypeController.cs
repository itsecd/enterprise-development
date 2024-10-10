using Microsoft.AspNetCore.Mvc;
using StoreCashFlow.Domain;
using StoreCashFlow.Api.Service;

namespace StoreCashFlow.Api.Controller;

/// <summary>
/// Контроллер для работы с типами товаров
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductTypeController(ProductTypeService productTypeService) : ControllerBase
{
    /// <summary>
    /// Получить все типы товаров
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<ProductType>> GetProductTypes()
    {
        return Ok(productTypeService.GetProductTypes());
    }

    /// <summary>
    /// Получить тип товара по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<ProductType> GetProductType(int id)
    {
        return Ok(productTypeService.GetProductTypeById(id));
    }

    /// <summary>
    /// Добавить тип товара
    /// </summary>
    [HttpPost]
    public ActionResult<ProductType> AddProductType(ProductType newProductType)
    {
        return Ok(productTypeService.AddProductType(newProductType));
    }

    /// <summary>
    /// Изменить тип товара
    /// </summary>
    [HttpPut]
    public IActionResult UpdateProductType(ProductType productType)
    {
        var result = productTypeService.UpdateProductType(productType);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    /// <summary>
    /// Удалить тип товара
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteProductType(int id)
    {
        var result = productTypeService.DeleteProductType(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
