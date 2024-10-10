using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

/// <summary>
/// Контроллер для предприятий
/// </summary>
/// <param name="service"></param>
[Route("api/enterprise")]
[ApiController]
public class EnterpriseController(EnterpriseService service): ControllerBase
{
    /// <summary>
    /// Получение всех предприятий
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<EnterpriseDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получение предприятия по регистрационному номеру
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<EnterpriseDto> Get(int id)
    {
        var enterpriseDto = service.GetByRegistrationNumber(id);
        if (enterpriseDto == null)
            return NotFound();
        return Ok(enterpriseDto);
    }

    /// <summary>
    /// Создание предприятия
    /// </summary>
    [HttpPost]
    public ActionResult<EnterpriseDto> Post(EnterpriseCreateDto enterpriseDto)
    {
        return Ok(service.Create(enterpriseDto));
    }

    /// <summary>
    /// Обновление информации о предприятии
    /// </summary>
    [HttpPut]
    public ActionResult<EnterpriseDto> Put(EnterpriseDto enterpriseDto)
    {
        return Ok(service.Update(enterpriseDto));
    }

    /// <summary>
    /// Удаление предприятия
    /// </summary>
    [HttpDelete("{id:int}")]
    public ActionResult<EnterpriseDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }

    /// <summary>
    /// Получение топ 5 предприятий по количеству поставок
    /// </summary>
    [HttpGet("five-most-supplied-enterprises")]
    public ActionResult<IEnumerable<EnterpriseDto>> GetFiveMostSuppliedEnterprises()
    {
        return Ok(service.FiveMostSuppliedEnterprises());
    }
}