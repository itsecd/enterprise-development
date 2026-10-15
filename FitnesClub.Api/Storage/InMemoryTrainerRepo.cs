using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Storage;

public class InMemoryTrainerRepo : IRepository <Trainer, Guid>
{
    private readonly List<Trainer> _trainers;

    public InMemoryTrainerRepo()
    {
        _trainers = [.. DataSeeder.Trainers];
    }
    public void Create(Trainer trainer)
    {
        _trainers.Add(trainer);
    }

    public List<Trainer> ReadAll()
    {
        return [.. _trainers];
    }

    public Trainer? Read(Guid id)
    {
        return _trainers.FirstOrDefault(t => t.Id == id);
    }

    public void Delete(Guid id)
    {
        var item = Read(id);
        if (item is not null)
        {
            _trainers.Remove(item);
        }
    }

    public void Update(Trainer updatedTrainer)
    {
        var index = _trainers.FindIndex(t => t.Id == updatedTrainer.Id);
        if (index != -1)
        {
            _trainers[index] = updatedTrainer;
        }
    }
}
