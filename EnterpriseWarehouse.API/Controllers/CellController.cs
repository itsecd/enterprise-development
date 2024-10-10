using EnterpriseWarehouse.API.Services;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CellController(CellService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Cell>> GetCells()
    {
        return Ok(service.GetCells());
    }

    [HttpGet("{id}")]
    public ActionResult<Cell> GetById(int id)
    {
        return Ok(service.GetCellById(id));
    }

    [HttpPost]
    public ActionResult<Cell> AddCell(Cell newCell)
    {
        return Ok(service.AddCell(newCell));
    }

    [HttpPut]
    public ActionResult UpdateCell(Cell newCell)
    {
        var result = service.UpdateCell(newCell);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete]
    public ActionResult DeleteCell(int id)
    {
        var result = service.DeleteCell(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
