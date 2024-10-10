using Microsoft.AspNetCore.Mvc;
using StoreCashFlow.Api.Service;
using StoreCashFlow.Domain;
namespace StoreCashFlow.Api.Controller;

/// <summary>
/// Контроллер для работы с продажами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class SaleController(SaleService saleService) : ControllerBase
{
    /// <summary>
    /// Получить все продажи
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Sale>> GetSales()
    {
        return Ok(saleService.GetSales());
    }

    /// <summary>
    /// Получить продажу по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<Sale> GetSale(int id)
    {
        return Ok(saleService.GetSaleById(id));
    }

    /// <summary>
    /// Добавить продажу
    /// </summary>
    [HttpPost]
    public ActionResult<Sale> AddSale(Sale newSale)
    {
        return Ok(saleService.AddSale(newSale));
    }

    /// <summary>
    /// Изменить продажу
    /// </summary>
    [HttpPut]
    public IActionResult UpdateSale(Sale sale)
    {
        var result = saleService.UpdateSale(sale);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    /// <summary>
    /// Удалить продажу
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteSale(int id)
    {
        var result = saleService.DeleteSale(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
