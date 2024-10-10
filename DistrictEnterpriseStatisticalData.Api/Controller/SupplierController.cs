using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

/// <summary>
/// Контроллер для поставщиков
/// </summary>
[Route("api/supplier")]
[ApiController]
public class SupplierController(SupplierService service): ControllerBase
{
    /// <summary>
    /// Получение всех поставщиков
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<SupplierDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получение поставщика по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<SupplierDto> Get(int id)
    {
        var supplierDto = service.GetById(id);
        if (supplierDto == null)
            return NotFound();
        return Ok(supplierDto);
    }

    /// <summary>
    /// Создание поставщика
    /// </summary>
    [HttpPost]
    public ActionResult<SupplierDto> Post(SupplierCreateDto supplierDto)
    {
        return Ok(service.Create(supplierDto));
    }

    /// <summary>
    /// Обновление информации о поставщике
    /// </summary>
    [HttpPut]
    public ActionResult<SupplierDto> Put(SupplierDto supplierDto)
    {
        return Ok(service.Update(supplierDto));
    }

    /// <summary>
    /// Удаление поставщика
    /// </summary>
    [HttpDelete("{id:int}")]
    public ActionResult<SupplierDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }

    /// <summary>
    /// Получение информации о всех поставщиках, поставивших сырье за заданных период, упорядоченных по названию
    /// </summary>
    [HttpGet("supply-between-dates")]
    public ActionResult<IEnumerable<SupplierDto>> GetSupplierBetweenDates(DateOnly startDate, DateOnly endDate)
    {
        return Ok(service.ReturnSupplyBetweenDates(startDate, endDate));
    }

    /// <summary>
    /// Получение информации о количестве предприятий, с которым работает каждый поставщик
    /// </summary>
    [HttpGet("enterprise-count-for-each-supplier")]
    public ActionResult<IEnumerable<EnterpriseCountForSupplierDto>> GetEnterpriseCountForSupplier()
    {
        return Ok(service.ReturnEnterprisesCountForEachSupplier());
    }

    /// <summary>
    /// Получение информации о количестве поставщиков для каждого типа отрасли и форме собственности
    /// </summary>
    [HttpGet("suppliers-count-for-type-and-form")]
    public ActionResult<IEnumerable<SupplierCountForTypeAndFormDto>> GetSupplierCountForTypeAndForm()
    {
        return Ok(service.ReturnSuppliersCountForTypeAndForm());
    }

    /// <summary>
    /// Получение информации о поставщиках, поставивших максимальное количество товара за указанный период
    /// </summary>
    [HttpGet("max-provided-suppliers")]
    public ActionResult<IEnumerable<SupplierDto>> GetMaxProvidedSuppliers(DateOnly startDate, DateOnly endDate)
    {
        return Ok(service.MaxProvidedSuppliers(startDate, endDate));
    }
}