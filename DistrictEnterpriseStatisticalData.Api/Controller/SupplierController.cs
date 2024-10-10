using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

[Route("api/supplier")]
[ApiController]
public class SupplierController(SupplierService service): ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<SupplierDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<SupplierDto> Get(int id)
    {
        var supplierDto = service.GetById(id);
        if (supplierDto == null)
            return NotFound();
        return Ok(supplierDto);
    }

    [HttpPost]
    public ActionResult<SupplierDto> Post(SupplierDto supplierDto)
    {
        return Ok(service.Create(supplierDto));
    }

    [HttpPut]
    public ActionResult<SupplierDto> Put(SupplierDto supplierDto)
    {
        return Ok(service.Update(supplierDto));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<SupplierDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }

    [HttpGet("supply-between-dates")]
    public ActionResult<IEnumerable<SupplierDto>> GetSupplierBetweenDates(DateOnly startDate, DateOnly endDate)
    {
        return Ok(service.ReturnSupplyBetweenDates(startDate, endDate));
    }

    [HttpGet("enterprise-count-for-each-supplier")]
    public ActionResult<IEnumerable<EnterpriseCountForSupplierDto>> GetEnterpriseCountForSupplier()
    {
        return Ok(service.ReturnEnterprisesCountForEachSupplier());
    }

    [HttpGet("suppliers-count-for-type-and-form")]
    public ActionResult<IEnumerable<SupplierCountForTypeAndFormDto>> GetSupplierCountForTypeAndForm()
    {
        return Ok(service.ReturnSuppliersCountForTypeAndForm());
    }

    [HttpGet("max-provided-suppliers")]
    public ActionResult<IEnumerable<SupplierDto>> GetMaxProvidedSuppliers(DateOnly startDate, DateOnly endDate)
    {
        return Ok(service.MaxProvidedSuppliers(startDate, endDate));
    }
}