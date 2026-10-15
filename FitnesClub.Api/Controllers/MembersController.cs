using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Members;
using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IRepository<Member, Guid> _repo;

    public MembersController(IRepository<Member, Guid> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<MemberDto>> GetAll()
    {
        var members = _repo.ReadAll();
        return Ok(members.Select(m => m.ToDto()).ToList());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<MemberDto> GetById(Guid id)
    {
        var member = _repo.Read(id);
        if (member is null)
            return NotFound($"Клиент с ID {id} не найден.");

        return Ok(member.ToDto());
    }

    [HttpPost]
    public ActionResult<MemberDto> Create([FromBody] MemberCreateUpdateDto dto)
    {
        var newMember = dto.ToEntity();
        _repo.Create(newMember);

        return CreatedAtAction(nameof(GetById), new { id = newMember.Id }, newMember.ToDto());
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] MemberCreateUpdateDto dto)
    {
        var existingMember = _repo.Read(id);
        if (existingMember is null)
            return NotFound($"Клиент с ID {id} не найден.");

        dto.UpdateEntity(existingMember);
        _repo.Update(existingMember);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var existingMember = _repo.Read(id);
        if (existingMember is null)
            return NotFound($"Клиент с ID {id} не найден.");

        _repo.Delete(id);
        return NoContent();
    }
}