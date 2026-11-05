using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Infrastructure.InMemory.Repositories;

public sealed class SpecializationRepository(InMemoryClinicStore store) : ISpecializationRepository
{
    public Task<IReadOnlyList<Specialization>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Specialization>>([.. store.Specializations]);
        }
    }

    public Task<Specialization?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult(store.Specializations.FirstOrDefault(specialization => specialization.Id == id));
        }
    }

    public Task<IReadOnlyList<Specialization>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Specialization>>([.. store.Specializations.Where(specialization => specialization.Species == species)]);
        }
    }

    public Task<Specialization> AddAsync(string name, AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var specialization = new Specialization
            {
                Id = store.TakeSpecializationId(),
                Name = name,
                Species = species
            };
            store.Specializations.Add(specialization);
            return Task.FromResult(specialization);
        }
    }

    public Task<Specialization> UpdateAsync(int id, string name, AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var specialization = store.Specializations.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("специализация", id);

            if (specialization.Species != species && HasAppointmentOfAnotherSpecies(id, species))
            {
                throw new DomainRuleException(
                    "Нельзя сменить вид специализации: у врачей этого профиля есть приёмы животных другого вида.");
            }

            specialization.Name = name;
            specialization.Species = species;
            return Task.FromResult(specialization);
        }
    }

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var specialization = store.Specializations.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("специализация", id);

            if (store.Veterinarians.Any(veterinarian => veterinarian.Specialization.Id == id))
            {
                throw new EntityInUseException(
                    $"Специализация «{specialization.Name}» назначена врачам, удаление невозможно.");
            }

            store.Specializations.Remove(specialization);
        }

        return Task.CompletedTask;
    }

    private bool HasAppointmentOfAnotherSpecies(int specializationId, AnimalSpecies species)
    {
        return store.Appointments.Any(appointment =>
            appointment.Veterinarian.Specialization.Id == specializationId
            && appointment.Pet.Species != species);
    }
}
