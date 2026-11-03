using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;
using FitnesClub.Data.Data;

namespace FitnesClub.Data.Repositories;

/// <summary>
/// Репозиторий для хранения и управления данными тренеров в оперативной памяти.
/// </summary>
public class InMemoryTrainerRepo : IRepository<Trainer, Guid>
{
    private readonly List<Trainer> _trainers = [.. DataSeeder.Trainers];

    /// <inheritdoc />
    public void Create(Trainer trainer)
    {
        if (trainer.Id == Guid.Empty)
        {
            trainer.Id = Guid.NewGuid();
        }

        _trainers.Add(trainer);
    }

    /// <inheritdoc />
    public List<Trainer> ReadAll()
    {
        return [.. _trainers];
    }

    /// <inheritdoc />
    public Trainer? Read(Guid id)
    {
        return _trainers.FirstOrDefault(t => t.Id == id);
    }

    /// <inheritdoc />
    public void Update(Trainer updatedTrainer)
    {
        var index = _trainers.FindIndex(t => t.Id == updatedTrainer.Id);
        if (index != -1)
        {
            _trainers[index] = updatedTrainer;
        }
    }

    /// <inheritdoc />
    public void Delete(Guid id)
    {
        _trainers.RemoveAll(t => t.Id == id);
    }
}