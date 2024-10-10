using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

/// <summary>
/// Контроллер поставок
/// </summary>
[Route("api/supply")]
[ApiController]
public class SupplyController(SupplyService service): ControllerBase
{
    /// <summary>
    /// Получение всех поставок
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<SupplyDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получение поставки по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<SupplyDto> Get(int id)
    {
        var supplyDto = service.GetById(id);
        if (supplyDto == null)
            return NotFound();
        return Ok(supplyDto);
    }

    /// <summary>
    /// Создание поставки
    /// </summary>
    [HttpPost]
    public ActionResult<SupplyDto> Post(SupplyCreateDto supplyDto)
    {
        return Ok(service.Create(supplyDto));
    }

    /// <summary>
    /// Обновление информации о поставке
    /// </summary>
    [HttpPut]
    public ActionResult<SupplyDto> Put(SupplyDto supplyDto)
    {
        return Ok(service.Update(supplyDto));
    }

    /// <summary>
    /// Удаление поставки
    /// </summary>
    [HttpDelete("{id:int}")]
    public ActionResult<SupplyDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }
}