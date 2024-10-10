using EnterpriseWarehouse.API.Services;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplyController(SupplyService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Supply>> GetSupplies()
    {
        return Ok(service.GetSupplies());
    }

    [HttpGet("{id}")]
    public ActionResult<Supply> GetById(int id)
    {
        return Ok(service.GetSupplyById(id));
    }

    [HttpPost]
    public ActionResult<Supply> AddSupply(Supply newSupply)
    {
        return Ok(service.AddSupply(newSupply));
    }

    [HttpPut]
    public ActionResult UpdateSupply(Supply newSupply)
    {
        var result = service.UpdateSupply(newSupply);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete]
    public ActionResult DeleteSupply(int id)
    {
        var result = service.DeleteSupply(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
