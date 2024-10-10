using StoreCashFlow.Domain;
using Microsoft.AspNetCore.Mvc;
using StoreCashFlow.Api.Service;
using StoreCashFlow.Api.DTO;

namespace StoreCashFlow.Api.Controller;

/// <summary>
/// Запросы
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RequestController(RequestService requestService) : ControllerBase
{
    /// <summary>
    /// Выводит сведения о всех товарах в заданном магазине
    /// </summary>
    [HttpGet("return-all-products-in-store/{id}")]
    public ActionResult<IEnumerable<Product>> ReturnAllProductsInStore(int id)
    {
        return Ok(requestService.ReturnAllProductsInStore(id));
    }

    /// <summary>
    /// Для заданного товара выводит список магазинов, в котором он находится в наличии
    /// </summary>
    [HttpGet("return-stores-with-product-in-stoke/{barcode}")]
    public ActionResult<IEnumerable<Store>> ReturnStoresWithProductInStock(string barcode)
    {
        return Ok(requestService.ReturnStoresWithProductInStock(barcode));
    }

    /// <summary>
    /// Выводит информацию о средней стоимости товаров каждой товарной группы для каждого магазина
    /// </summary>
    [HttpGet("return-average-price-by-group-and-store")]
    public ActionResult<IEnumerable<ProductPriceInfoDto>> ReturnAveragePriceByGroupAndStore()
    {
        return Ok(requestService.ReturnAveragePriceByGroupAndStore());
    }

    /// <summary>
    /// Выводит топ 5 покупок по общей сумме продажи
    /// </summary>
    [HttpGet("return-top-5-sales-by-total-amount")]
    public ActionResult<IEnumerable<SaleInfoDto>> ReturnTop5SalesByTotalAmount()
    {
        return Ok(requestService.ReturnTop5SalesByTotalAmount());
    }

    /// <summary>
    /// Выводит все сведения о товарах, превышающих предельную дату хранения, с указанием магазина
    /// </summary>
    [HttpGet("return-expired-products/{expirationDate}")]
    public ActionResult<IEnumerable<ExpiredProductInfoDto>> ReturnExpiredProducts(DateTime expirationDate)
    {
        return Ok(requestService.ReturnExpiredProducts(expirationDate));
    }

    /// <summary>
    /// Выводит список магазинов, в которых за месяц было продано товаров на сумму, превышающую заданную
    /// </summary>
    [HttpGet("get-stores-with-high-sales/{monfStart}/{monfEnd}/{treshold}")]
    public ActionResult<IEnumerable<HighSalesDto>> GetStoresWithHighSales(DateTime monfStart, DateTime monfEnd, double treshold )
    {
        return Ok(requestService.GetStoresWithHighSales(monfStart, monfEnd, treshold));
    }
}
