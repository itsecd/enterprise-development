using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Storage;

/// <summary>
/// Репозиторий для хранения и управления данными тренеров в оперативной памяти.
/// </summary>
public class InMemoryTrainerRepo : IRepository<Trainer, Guid>
{
    private readonly List<Trainer> _trainers;

    /// <summary>
    /// Инициализирует репозиторий тренеров начальными данными из <see cref="DataSeeder"/>.
    /// </summary>
    public InMemoryTrainerRepo()
    {
        _trainers = [.. DataSeeder.Trainers];
    }

    /// <inheritdoc />
    public void Create(Trainer trainer)
    {
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
    public void Delete(Guid id)
    {
        var item = Read(id);
        if (item is not null)
        {
            _trainers.Remove(item);
        }
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
}