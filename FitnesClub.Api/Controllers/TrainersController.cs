using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Trainers;
using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;


namespace FitnesClub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly IRepository<Trainer, Guid> _repo;

    public TrainersController(IRepository<Trainer, Guid> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<TrainerDto>> GetAll()
    {
        var trainers = _repo.ReadAll();
        return Ok(trainers.Select(t => t.ToDto()).ToList());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<TrainerDto> GetById(Guid id)
    {
        var trainer = _repo.Read(id);
        if (trainer is null)
            return NotFound($"Тренер с ID {id} не найден.");
        return Ok(trainer.ToDto());
    }

    [HttpPost]
    public ActionResult<TrainerDto> Create([FromBody] TrainersCreateUpdateDto dto)
    {
        var newTrainer = dto.ToEntity();
        _repo.Create(newTrainer);
        return CreatedAtAction(nameof(GetById), new { id = newTrainer.Id } , newTrainer.ToDto());
    }

    [HttpPut("{id:guid}")]
    public ActionResult Update(Guid id, [FromBody] TrainersCreateUpdateDto dto)
    {
        var existingTrainer = _repo.Read(id);
        if (existingTrainer is null)
            return NotFound($"Тренер с ID {id} не найден.");
        
        dto.UpdateEntity(existingTrainer);
        _repo.Update(existingTrainer);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var existingTrainer = _repo.Read(id);
        if (existingTrainer is null)
            return NotFound($"Тренер с ID {id} не найден.");

        _repo.Delete(id);
        return NoContent();
    }
}
