using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.API.Services;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueryController(QueryService service) : ControllerBase
{
    [HttpGet("sorted-products")]
    public ActionResult<IEnumerable<Product?>> GetAllProductsSortedByName()
    {
        return Ok(service.GetAllProductsSortedByName());
    }

    [HttpGet("products-recive/{name}/{date}")]
    public ActionResult<IEnumerable<Product?>> GetProductsRecieveOnDate(string name, DateTime date)
    {
        return Ok(service.GetProductsRecieveOnDate(name, date));
    }

    [HttpGet("warehouse-state")]
    public ActionResult<IEnumerable<Cell>> GetCurrentWarehouseState()
    {
        return Ok(service.GetCurrentWarehouseState());
    }

    [HttpGet("max-supplies/{startDate}/{endDate}")]
    public ActionResult<IEnumerable<Organization>> GetMaxSuppliesOrganizations(DateTime startDate, DateTime endDate)
    {
        return Ok(service.GetMaxSuppliesOrganizations(startDate, endDate));
    }

    [HttpGet("five-max-quantity-products")]
    public ActionResult<IEnumerable<ProductQuantityDTO>> GetFiveMaxQuantityProducts()
    {
        return Ok(service.GetFiveMaxQuantityProducts());
    }

    [HttpGet("get-quantity-product-supply-to-organizations")]
    public ActionResult<IEnumerable<ProductSupplyToOrganizationsDTO>> GetQuantityProductSupplyToOrganiztions()
    {
        return Ok(service.GetQuantityProductSupplyToOrganiztions());
    }
}
