using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

[Route("api/enterprise")]
[ApiController]
public class EnterpriseController(EnterpriseService service): ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<EnterpriseDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<EnterpriseDto> Get(int id)
    {
        var enterpriseDto = service.GetByRegistrationNumber(id);
        if (enterpriseDto == null)
            return NotFound();
        return Ok(enterpriseDto);
    }

    [HttpPost]
    public ActionResult<EnterpriseDto> Post(EnterpriseDto enterpriseDto)
    {
        return Ok(service.Create(enterpriseDto));
    }

    [HttpPut]
    public ActionResult<EnterpriseDto> Put(EnterpriseDto enterpriseDto)
    {
        return Ok(service.Update(enterpriseDto));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<EnterpriseDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }

    [HttpGet("five-most-supplied-enterprises")]
    public ActionResult<IEnumerable<EnterpriseDto>> GetFiveMostSuppliedEnterprises()
    {
        return Ok(service.FiveMostSuppliedEnterprises());
    }
}