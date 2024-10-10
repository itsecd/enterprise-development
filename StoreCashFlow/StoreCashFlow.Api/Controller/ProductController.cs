using Microsoft.AspNetCore.Mvc;
using StoreCashFlow.Api.Service;
using StoreCashFlow.Domain;

namespace StoreCashFlow.Api.Controller;

/// <summary>
/// Контроллер для работы с товарами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductService productService) : ControllerBase
{
    /// <summary>
    ///  Получить все товары
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(productService.GetProducts());
    }

    /// <summary>
    /// Добавить товар
    /// </summary>
    [HttpPost]
    public ActionResult<Product> AddProduct(Product newProduct)
    {
        return Ok(productService.AddProduct(newProduct));
    }

    /// <summary>
    /// Получить товар по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(string id)
    {
        return Ok(productService.GetProductById(id));
    }

    /// <summary>
    /// Изменить товар
    /// </summary>
    [HttpPut]
    public IActionResult UpdateProduct(Product product)
    {
        var result = productService.UpdateProduct(product);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    /// <summary>
    /// Удалить товар
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(string id)
    {
        var result = productService.DeleteProduct(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}