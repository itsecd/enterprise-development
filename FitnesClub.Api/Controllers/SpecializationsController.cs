using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Specializations;
using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecializationsController : ControllerBase
{
    private readonly IRepository<Specialization, Guid> _repo;

    public SpecializationsController(IRepository<Specialization, Guid> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<SpecializationDto>> GetAll()
    {
        var specializations = _repo.ReadAll();
        return Ok(specializations.Select(s => s.ToDto()).ToList());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<SpecializationDto> GetById(Guid id)
    {
        var specialization = _repo.Read(id);
        if (specialization is null)
            return NotFound($"Специализация с ID {id} не найдена.");

        return Ok(specialization.ToDto());
    }

    [HttpPost]
    public ActionResult<SpecializationDto> Create([FromBody] SpecializationCreateUpdateDto dto)
    {
        var newSpecialization = dto.ToEntity();
        _repo.Create(newSpecialization);

        return CreatedAtAction(nameof(GetById), new { id = newSpecialization.Id }, newSpecialization.ToDto());
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] SpecializationCreateUpdateDto dto)
    {
        var existing = _repo.Read(id);
        if (existing is null)
            return NotFound($"Специализация с ID {id} не найдена.");

        dto.UpdateEntity(existing);
        _repo.Update(existing);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var existing = _repo.Read(id);
        if (existing is null)
            return NotFound($"Специализация с ID {id} не найдена.");

        _repo.Delete(id);
        return NoContent();
    }
}