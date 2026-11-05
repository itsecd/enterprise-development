using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Infrastructure.InMemory.Repositories;

public sealed class VeterinarianRepository(InMemoryClinicStore store) : IVeterinarianRepository
{
    public Task<IReadOnlyList<Veterinarian>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Veterinarian>>([.. store.Veterinarians]);
        }
    }

    public Task<Veterinarian?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult(store.Veterinarians.FirstOrDefault(veterinarian => veterinarian.Id == id));
        }
    }

    public Task<IReadOnlyList<Veterinarian>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Veterinarian>>([.. store.Veterinarians
                .Where(veterinarian => veterinarian.Specialization.Species == species)]);
        }
    }

    public Task<IReadOnlyList<Veterinarian>> GetBySpecializationAsync(int specializationId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            _ = store.Specializations.FirstOrDefault(specialization => specialization.Id == specializationId)
                ?? throw new EntityNotFoundException("специализация", specializationId);

            return Task.FromResult<IReadOnlyList<Veterinarian>>([.. store.Veterinarians
                .Where(veterinarian => veterinarian.Specialization.Id == specializationId)]);
        }
    }

    public Task<Veterinarian> AddAsync(
        string passportNumber,
        string fullName,
        int specializationId,
        int birthYear,
        int experienceYears,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var veterinarian = new Veterinarian
            {
                Id = store.TakeVeterinarianId(),
                PassportNumber = passportNumber,
                FullName = fullName,
                Specialization = FindSpecialization(specializationId),
                BirthYear = birthYear,
                ExperienceYears = experienceYears
            };
            store.Veterinarians.Add(veterinarian);
            return Task.FromResult(veterinarian);
        }
    }

    public Task<Veterinarian> UpdateAsync(
        int id,
        string passportNumber,
        string fullName,
        int specializationId,
        int birthYear,
        int experienceYears,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var veterinarian = store.Veterinarians.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("ветеринар", id);
            var specialization = FindSpecialization(specializationId);

            if (store.Appointments.Any(appointment =>
                    appointment.Veterinarian.Id == id
                    && appointment.Pet.Species != specialization.Species))
            {
                throw new DomainRuleException(
                    "Нельзя сменить специализацию: есть приёмы питомцев другого вида.");
            }

            veterinarian.PassportNumber = passportNumber;
            veterinarian.FullName = fullName;
            veterinarian.Specialization = specialization;
            veterinarian.BirthYear = birthYear;
            veterinarian.ExperienceYears = experienceYears;
            return Task.FromResult(veterinarian);
        }
    }

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var veterinarian = store.Veterinarians.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("ветеринар", id);

            if (store.Appointments.Any(appointment => appointment.Veterinarian.Id == id))
            {
                throw new EntityInUseException(
                    $"У врача «{veterinarian.FullName}» есть записи на приём, удаление невозможно.");
            }

            store.Veterinarians.Remove(veterinarian);
        }

        return Task.CompletedTask;
    }

    private Specialization FindSpecialization(int specializationId) =>
        store.Specializations.FirstOrDefault(specialization => specialization.Id == specializationId)
        ?? throw new EntityNotFoundException("специализация", specializationId);
}
