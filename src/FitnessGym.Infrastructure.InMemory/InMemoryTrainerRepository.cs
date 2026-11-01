using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory;

/// <summary>
/// Репозиторий сущностей Trainer с хранением в памяти.
/// </summary>
/// <param name="seed">Начальные данные.</param>
public class InMemoryTrainerRepository(IReadOnlyList<Trainer> seed)
    : InMemoryRepository<Trainer>(seed), ITrainerRepository
{
    /// <inheritdoc/>
    public Task<IReadOnlyList<Trainer>> GetExperiencedAsync(int minYears) =>
        ReadAsync<IReadOnlyList<Trainer>>(items =>
            [.. items.Where(trainer => trainer.ExperienceYears >= minYears)]);

    /// <inheritdoc/>
    public Task<bool> HasSpecializationAsync(int specializationId) =>
        ReadAsync(items => items.Any(trainer => trainer.Specialization.Id == specializationId));

    /// <inheritdoc/>
    protected override void CopyProperties(Trainer source, Trainer target)
    {
        target.PassportNumber = source.PassportNumber;
        target.FullName = source.FullName;
        target.Gender = source.Gender;
        target.BirthDate = source.BirthDate;
        target.Specialization = source.Specialization;
        target.ExperienceYears = source.ExperienceYears;
    }
}
