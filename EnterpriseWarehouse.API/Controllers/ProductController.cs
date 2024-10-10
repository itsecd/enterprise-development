using EnterpriseWarehouse.API.Services;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ProductService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(service.GetProducts());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        return Ok(service.GetProductById(id));
    }

    [HttpPost]
    public ActionResult<Product> AddProduct(Product newProduct)
    {
        return Ok(service.AddProduct(newProduct));
    }

    [HttpPut]
    public ActionResult UpdateProduct(Product newProduct)
    {
        var result = service.UpdateProduct(newProduct);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete]
    public ActionResult DeleteProduct(int id)
    {
        var result = service.DeleteProduct(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
