using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

/// <summary>
/// Контроллер для типов предприятий
/// </summary>
[Route("api/enterprise-type")]
[ApiController]
public class EnterpriseTypeController(EnterpriseTypeService service): ControllerBase
{
    /// <summary>
    /// Получение всех типов предприятий
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<EnterpriseTypeDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получение типа по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<EnterpriseTypeDto> Get(int id)
    {
        var enterpriseTypeDto = service.GetById(id);
        if (enterpriseTypeDto == null)
            return NotFound();
        return Ok(enterpriseTypeDto);
    }

    /// <summary>
    /// Создание типа предприятия
    /// </summary>
    [HttpPost]
    public ActionResult<EnterpriseTypeDto> Post(EnterpriseTypeCreateDto enterpriseTypeDto)
    {
        return Ok(service.Create(enterpriseTypeDto));
    }

    /// <summary>
    /// Обновление информации о типе предприятия
    /// </summary>
    [HttpPut]
    public ActionResult<EnterpriseTypeDto> Put(EnterpriseTypeDto enterpriseTypeDto)
    {
        return Ok(service.Update(enterpriseTypeDto));
    }

    /// <summary>
    /// Удаление типа предприятия
    /// </summary>
    [HttpDelete("{id:int}")]
    public ActionResult<EnterpriseTypeDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }
}