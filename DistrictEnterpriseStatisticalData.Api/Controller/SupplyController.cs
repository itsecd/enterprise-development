using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

[Route("api/supply")]
[ApiController]
public class SupplyController(SupplyService service): ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<SupplyDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<SupplyDto> Get(int id)
    {
        var supplyDto = service.GetById(id);
        if (supplyDto == null)
            return NotFound();
        return Ok(supplyDto);
    }

    [HttpPost]
    public ActionResult<SupplyDto> Post(SupplyDto supplyDto)
    {
        return Ok(service.Create(supplyDto));
    }

    [HttpPut]
    public ActionResult<SupplyDto> Put(SupplyDto supplyDto)
    {
        return Ok(service.Update(supplyDto));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<SupplyDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }
}