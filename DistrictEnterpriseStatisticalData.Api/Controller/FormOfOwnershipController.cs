using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DistrictEnterpriseStatisticalData.Api.Controller;

/// <summary>
/// Контроллер для форм собственности
/// </summary>
[Route("api/form-of-ownership")]
[ApiController]
public class FormOfOwnershipController(FormOfOwnershipService service): ControllerBase
{
    /// <summary>
    /// Получение всех форм
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<FormOfOwnershipDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получение формы по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<FormOfOwnershipDto> Get(int id)
    {
        var formOfOwnershipDto = service.GetById(id);
        if (formOfOwnershipDto == null)
            return NotFound();
        return Ok(formOfOwnershipDto);
    }

    /// <summary>
    /// Создание формы
    /// </summary>
    [HttpPost]
    public ActionResult<FormOfOwnershipDto> Post(FormOfOwnershipCreateDto formOfOwnershipDto)
    {
        return Ok(service.Create(formOfOwnershipDto));
    }

    /// <summary>
    /// Обновление информации о форме
    /// </summary>
    [HttpPut]
    public ActionResult<FormOfOwnershipDto> Put(FormOfOwnershipDto formOfOwnershipDto)
    {
        return Ok(service.Update(formOfOwnershipDto));
    }

    /// <summary>
    /// Удаление форме
    /// </summary>
    [HttpDelete("{id:int}")]
    public ActionResult<FormOfOwnershipDto> Delete(int id)
    {
        service.Delete(id);
        return Ok();
    }
}