using Microsoft.AspNetCore.Mvc;
using StoreCashFlow.Api.Service;
using StoreCashFlow.Domain;

namespace StoreCashFlow.Api.Controller;

/// <summary>
/// Контроллер для работы с доступностью товаров
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductAvailabilityController(ProductAvailabilityService availabilityService) : ControllerBase
{
    /// <summary>
    /// Получить все доступные товаров
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<ProductAvailability>> GetProductAvailabilities()
    {
        return Ok(availabilityService.GetProductAvailabilities());
    }

    /// <summary>
    /// Получить доступность товара по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<ProductAvailability> GetProductAvailability(int id)
    {
        return Ok(availabilityService.GetProductAvailabilityById(id));
    }

    /// <summary>
    /// Добавить доступность товара
    /// </summary>
    [HttpPost]
    public ActionResult<ProductAvailability> AddProductAvailability(ProductAvailability newProductAvailability)
    {
        return Ok(availabilityService.AddProductAvailability(newProductAvailability));
    }

    /// <summary>
    /// Изменить доступность товара
    /// </summary>
    [HttpPut]
    public IActionResult UpdateProductAvailability(ProductAvailability productAvailability)
    {
        var result = availabilityService.UpdateProductAvailability(productAvailability);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    /// <summary>
    /// Изменить доступность товара
    /// </summary>
    [HttpDelete]
    public IActionResult DeleteProductAvailability(int id)
    {
        var result = availabilityService.DeleteProductAvailability(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
