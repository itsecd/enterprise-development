using EnterpriseWarehouse.API.Services;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationController(OrganizationService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Organization>> GetOrganizations()
    {
        return Ok(service.GetOrganizations());
    }

    [HttpGet("{id}")]
    public ActionResult<Organization> GetById(int id)
    {
        return Ok(service.GetOrganizationById(id));
    }

    [HttpPost]
    public ActionResult<Organization> AddOrganization(Organization newOrganization)
    {
        return Ok(service.AddOrganization(newOrganization));
    }

    [HttpPut]
    public ActionResult UpdateOrganization(Organization newOrganization)
    {
        var result = service.UpdateOrganization(newOrganization);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete]
    public ActionResult DeleteOrganization(int id)
    {
        var result = service.DeleteOrganization(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
