using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory;

/// <summary>
/// Репозиторий сущностей Specialization с хранением в памяти.
/// </summary>
/// <param name="seed">Начальные данные.</param>
public class InMemorySpecializationRepository(IReadOnlyList<Specialization> seed)
    : InMemoryRepository<Specialization>(seed), ISpecializationRepository
{
    /// <inheritdoc/>
    protected override void CopyProperties(Specialization source, Specialization target)
    {
        target.Name = source.Name;
    }
}
