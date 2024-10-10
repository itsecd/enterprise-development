using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

[Route("api/enterprise-type")]
[ApiController]
public class EnterpriseTypeController(EnterpriseTypeService service): ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<EnterpriseTypeDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<EnterpriseTypeDto> Get(int id)
    {
        var enterpriseTypeDto = service.GetById(id);
        if (enterpriseTypeDto == null)
            return NotFound();
        return Ok(enterpriseTypeDto);
    }

    [HttpPost]
    public ActionResult<EnterpriseTypeDto> Post(EnterpriseTypeCreateDto enterpriseTypeDto)
    {
        return Ok(service.Create(enterpriseTypeDto));
    }

    [HttpPut]
    public ActionResult<EnterpriseTypeDto> Put(EnterpriseTypeDto enterpriseTypeDto)
    {
        return Ok(service.Update(enterpriseTypeDto));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<EnterpriseTypeDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }
}