using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

[Route("api/form-of-ownership")]
[ApiController]
public class FormOfOwnershipController(FormOfOwnershipService service): ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<FormOfOwnershipDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<FormOfOwnershipDto> Get(int id)
    {
        var formOfOwnershipDto = service.GetById(id);
        if (formOfOwnershipDto == null)
            return NotFound();
        return Ok(formOfOwnershipDto);
    }

    [HttpPost]
    public ActionResult<FormOfOwnershipDto> Post(FormOfOwnershipDto formOfOwnershipDto)
    {
        return Ok(service.Create(formOfOwnershipDto));
    }

    [HttpPut]
    public ActionResult<FormOfOwnershipDto> Put(FormOfOwnershipDto formOfOwnershipDto)
    {
        return Ok(service.Update(formOfOwnershipDto));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<FormOfOwnershipDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }
}