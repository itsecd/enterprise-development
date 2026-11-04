using BeautySalon.Contracts.Masters;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// CRUD-операции над мастерами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MastersController(IMasterRepository masterRepository) : ControllerBase
{
    /// <summary>
    /// Возвращает всех мастеров.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MasterResponse>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<MasterResponse>> GetAll()
    {
        return Ok(masterRepository.GetAll().Select(MapToResponse));
    }

    /// <summary>
    /// Возвращает мастера по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MasterResponse> GetById(int id)
    {
        var master = masterRepository.GetById(id);
        return master is null ? NotFound() : Ok(MapToResponse(master));
    }

    /// <summary>
    /// Создает нового мастера.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<MasterResponse> Create([FromBody] MasterRequest request)
    {
        var master = new Master
        {
            FullName = request.FullName,
            Gender = request.Gender,
            PassportNumber = request.PassportNumber,
            Specialization = request.Specialization,
            ExperienceYears = request.ExperienceYears
        };

        masterRepository.Add(master);
        
        return CreatedAtAction(nameof(GetById), new { id = master.Id }, MapToResponse(master));
    }

    /// <summary>
    /// Обновляет данные мастера по идентификатору.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] MasterRequest request)
    {
        var master = masterRepository.GetById(id);
        if (master is null) return NotFound();

        master.FullName = request.FullName;
        master.Gender = request.Gender;
        master.PassportNumber = request.PassportNumber;
        master.Specialization = request.Specialization;
        master.ExperienceYears = request.ExperienceYears;

        masterRepository.Update(master);
        return NoContent();
    }

    /// <summary>
    /// Удаляет мастера по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        var master = masterRepository.GetById(id);
        
        if (master is not null)
        {
            masterRepository.Delete(master);
        }

        return NoContent();
    }

    private static MasterResponse MapToResponse(Master master) => new()
    {
        Id = master.Id,
        FullName = master.FullName,
        Gender = master.Gender,
        PassportNumber = master.PassportNumber,
        Specialization = master.Specialization,
        ExperienceYears = master.ExperienceYears
    };
}